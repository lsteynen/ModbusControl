using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace LST.Helpers
{
    class ByteQueue
    {
        private Byte[] mBuffer;
        private int mSize;
        private int mHead;
        private int mTail;
        private int mCount;
        private Object mLock = new Object();

        // constructor
        public ByteQueue(int Size)
        {
            mBuffer = new Byte[Size];
            mSize = Size;
            mHead = 0;
            mTail = 0;
            mCount = 0;
        }

        // properties

        public int Count
        {
            get
            {
                return this.mCount;
            }
        }

        // methods 

        public void Clear()
        {
            lock (mLock)
            {
                mHead = 0;
                mTail = 0;
                mCount = 0;
            }
        }

        /* Add a single byte to the queue
         * Throws exception if queue is full
         */
        public void Add( byte b)
        {
            lock (mLock)
            {
                // size check
                if ((mCount + 1) > mSize)
                    throw new InvalidOperationException();

                // add byte
                mBuffer[mHead] = b;
                mHead = (mHead + 1) % mSize;
                mCount++;
            }
        }

        /* Add an array of bytes to the queue
         * Throws exception if queue is full
         */
        public void Add(byte[] arr, int count)
        {
            lock (mLock)
            {
                // size check
                if ((mCount + count) > mSize)
                    throw new InvalidOperationException();

                // copy part1 till end of circular buffer
                int count1 = Math.Min(count, mSize - mHead);
                Array.Copy(arr, 0, mBuffer, mHead, count1);
                // copy part2 to begin of circular buffer?
                if (count > count1)
                {
                    int count2 = count - count1;
                    Array.Copy(arr, count1, mBuffer, 0, count2);
                }
                mHead = (mHead + count) % mSize;
                mCount += count;
            }
        }

        /* Get a single byte
         * Throw exception is no byte available
         */
        public byte Get()
        {
            lock (mLock)
            {
                // size check
                if (mCount < 1)
                    throw new InvalidOperationException();

                // get byte
                byte b = mBuffer[mTail];
                mTail = (mTail + 1) % mSize;
                mCount--;
                return b;
            }
        }

        /*
         * Read a number of bytes from the queue,
         * Put them in an existing array, must be large enough,
         * Returns immedately with the number of bytes in the queue, 
         * with a max of maxcount bytes.
         */
        public int Get(ref byte[] buffer, int start, int maxcount)
        {
            int count;

            lock (mLock)
            {
                // # bytes to return
                count = Math.Min( mCount, maxcount);

                if (count > 0)
                {
                    // copy part1 till end of circular buffer
                    int count1 = Math.Min(count, mSize - mTail);
                    Array.Copy(mBuffer, mTail, buffer, start, count1);
                    // copy part2 from begin of circular buffer?
                    if (count > count1)
                    {
                        int count2 = count - count1;
                        Array.Copy(mBuffer, 0, buffer, start + count1, count2);
                    }
                    mTail = (mTail + count) % mSize;
                    mCount -= count;
                }
            }

            return count;
        }

        /*
         * Read all bytes from the queue,
         * return them in a new array
         */
        public byte[] GetAll()
        {
            byte[] retarr = null;

            lock (mLock)
            {
                retarr = new byte[mCount];

                if (mCount > 0)
                {
                    // copy part1 till end of circular buffer
                    int count1 = Math.Min(mCount, mSize - mTail);
                    Array.Copy(mBuffer, mTail, retarr, 0, count1);
                    // copy part2 from begin of circular buffer?
                    if (mCount > count1)
                    {
                        int count2 = mCount - count1;
                        Array.Copy(mBuffer, 0, retarr, count1, count2);
                    }
                    mTail = mHead;
                    mCount = 0;
                }
            }

            return retarr;
        }

        /*
         * Read count bytes from the queue,
         * Blocks is there are less bytes in the queue, with a timeout.
         * Return: the number of bytes returned.
         */
        public int GetWait(ref byte[] buffer, int count, int timeout)
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": ByteQueue: GetWait() enter");

            long starttime = DateTime.UtcNow.Ticks;

            // wait till we have enough bytes, with timeout
            while ((mCount < count) && (((DateTime.UtcNow.Ticks - starttime) / TimeSpan.TicksPerMillisecond) < timeout))
            {
                Thread.Sleep(10);
            }

            Debug.Print(DateTime.Now.TimeOfDay + ": ByteQueue: after while: mCount="+mCount+", count="+count+", time="+((DateTime.UtcNow.Ticks - starttime) / TimeSpan.TicksPerMillisecond));

            // how many bytes to return?
            count = Math.Min(count, mCount);

            // copy the bytes
            if (count > 0)
            {
                lock (mLock)
                {
                    // copy part1 till end of circular buffer
                    int count1 = Math.Min(count, mSize - mTail);
                    Array.Copy(mBuffer, mTail, buffer, 0, count1);
                    // copy part2 from begin of circular buffer?
                    if (count > count1)
                    {
                        int count2 = count - count1;
                        Array.Copy(mBuffer, 0, buffer, count1, count2);
                    }
                    mTail = (mTail + count) % mSize;
                    mCount -= count;
                }
            }

            Debug.Print(DateTime.Now.TimeOfDay + ": ByteQueue: return " + count);

            return count;
        }
    }
}
