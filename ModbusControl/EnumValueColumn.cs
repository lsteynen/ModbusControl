using System;
using System.Drawing;
using System.Windows.Forms;

// Source: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-host-controls-in-windows-forms-datagridview-cells?view=netframeworkdesktop-4.8&redirectedfrom=MSDN

public class EnumValueColumn : DataGridViewColumn
{
    public EnumValueColumn() : base(new EnumValueCell())
    {
    }

    public override DataGridViewCell CellTemplate
    {
        get
        {
            return base.CellTemplate;
        }
        set
        {
            // Ensure that the cell used for the template is a EnumValueCell.
            if (value != null &&
                !value.GetType().IsAssignableFrom(typeof(EnumValueCell)))
            {
                throw new InvalidCastException("Must be a EnumValueCell");
            }
            base.CellTemplate = value;
        }
    }
}

public class EnumValueCell : DataGridViewTextBoxCell
{
	public bool IsEnum = false;
    public bool AutoSizeDropdown = true;
	
    public EnumValueCell()
        : base()
    {
       
    }
   
    public override void InitializeEditingControl(int rowIndex, object
        initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        // Set the value of the editing control to the current cell value.
        base.InitializeEditingControl(rowIndex, initialFormattedValue,
            dataGridViewCellStyle);

		//kies of we textbox of combobox editeren
        if (IsEnum)
		{
	        EnumValueEditingControl ctl = DataGridView.EditingControl as EnumValueEditingControl;
            // Use the default row value when Value property is null.
            if ((this.Value == null) || (this.Value == DBNull.Value))
            {
                ctl.Text = (string)this.DefaultNewRowValue;
            }
            else
            {
                ctl.Text = (string)this.Value;
	        }

            ctl.DropDown -= OnDropdown;
            ctl.DropDown += OnDropdown;
        }
        else
        {
			TextBox textBox = base.DataGridView.EditingControl as TextBox;
            if (textBox != null)
            {
                // nothing to do
            }
        }
    }

    private void OnDropdown(object sender, EventArgs e)
    {
        // adjust dropdown width for widest dropdown item 
        if (AutoSizeDropdown)
        {
            ComboBox c = sender as ComboBox;
            int widest = MeasureWidestItemWidth(c);
            c.DropDownWidth = widest + 3;
        }
    }

    public int MeasureWidestItemWidth(ComboBox c)
    {
        int widest = 0;
        foreach (string it in c.Items)
        {
            int w = TextRenderer.MeasureText(it, c.Font).Width;
            if (w > widest)
                widest = w;
        }
        return widest;
    }

    public override Type EditType
    {
        get
        {
            // Return the type of the editing control that EnumValueCell uses.
            //kies of we Textbox of BomboBox editeren
            if (IsEnum)
			{
	            return typeof(EnumValueEditingControl);
			}
			else
			{
				return typeof(DataGridViewTextBoxEditingControl);
			}
        }
    }

    public override Type ValueType
    {
        get
        {
            // Return the type of the value that EnumValueCell contains.

            return typeof(string);
        }
    }

    public override object DefaultNewRowValue
    {
        get
        {
            // Use the current date and time as the default value.
            return "";
        }
    }
}

class EnumValueEditingControl : ComboBox, IDataGridViewEditingControl
{
    DataGridView dataGridView;
    private bool valueChanged = false;
    int rowIndex;

    public EnumValueEditingControl()
    {
        //this.Format = DateTimePickerFormat.Short;
    }

    // Implements the IDataGridViewEditingControl.EditingControlFormattedValue
    // property.
    public object EditingControlFormattedValue
    {
        get
        {
            return this.Text;
        }
        set
        {
            if (value is String)
            {
                this.Text = (string)value;
            }
        }
    }

    // Implements the
    // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
    public object GetEditingControlFormattedValue(
        DataGridViewDataErrorContexts context)
    {
        return EditingControlFormattedValue;
    }

    // Implements the
    // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
    public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
        this.Font = dataGridViewCellStyle.Font;
        //this.ForeColor = dataGridViewCellStyle.ForeColor;
        //this.BackColor = dataGridViewCellStyle.BackColor;
    }

    // Implements the IDataGridViewEditingControl.EditingControlRowIndex
    // property.
    public int EditingControlRowIndex
    {
        get
        {
            return rowIndex;
        }
        set
        {
            rowIndex = value;
        }
    }

    // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey
    // method.
    public bool EditingControlWantsInputKey(
        Keys key, bool dataGridViewWantsInputKey)
    {
        // Let the ComboBox handle the keys listed.
        switch (key & Keys.KeyCode)
        {
            case Keys.Left:
            case Keys.Up:
            case Keys.Down:
            case Keys.Right:
            case Keys.Home:
            case Keys.End:
            case Keys.PageDown:
            case Keys.PageUp:
                return true;
            default:
                return !dataGridViewWantsInputKey;
        }
    }

    // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit
    // method.
    public void PrepareEditingControlForEdit(bool selectAll)
    {
        // No preparation needs to be done.
    }

    // Implements the IDataGridViewEditingControl
    // .RepositionEditingControlOnValueChange property.
    public bool RepositionEditingControlOnValueChange
    {
        get
        {
            return false;
        }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingControlDataGridView property.
    public DataGridView EditingControlDataGridView
    {
        get
        {
            return dataGridView;
        }
        set
        {
            dataGridView = value;
        }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingControlValueChanged property.
    public bool EditingControlValueChanged
    {
        get
        {
            return valueChanged;
        }
        set
        {
            valueChanged = value;
        }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingPanelCursor property.
    public Cursor EditingPanelCursor
    {
        get
        {
            return base.Cursor;
        }
    }

    protected override void OnTextChanged(EventArgs eventargs)
    {
        // Notify the DataGridView that the contents of the cell
        // have changed.
        valueChanged = true;
        this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
        base.OnTextChanged(eventargs);
    }

}
