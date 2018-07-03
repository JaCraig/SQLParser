/*
Copyright 2017 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace SQLParser
{
    /// <summary>
    /// Changes the case of the char stream
    /// </summary>
    /// <seealso cref="ICharStream"/>
    public class CaseChangingCharStream : ICharStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaseChangingCharStream"/> class.
        /// </summary>
        /// <param name="internalStream">The stream.</param>
        public CaseChangingCharStream(ICharStream internalStream)
        {
            InternalStream = internalStream;
        }

        /// <summary>
        /// Return the index into the stream of the input symbol referred to by <c>LA(1)</c> .
        /// <p>
        /// The behavior of this method is unspecified if no call to an
        /// <see cref="T:Antlr4.Runtime.IIntStream">initializing method</see> has occurred after this
        /// stream was constructed.
        /// </p>
        /// </summary>
        public int Index => InternalStream.Index;

        /// <summary>
        /// Returns the total number of symbols in the stream, including a single EOF symbol.
        /// </summary>
        /// <remarks>
        /// Returns the total number of symbols in the stream, including a single EOF symbol.
        /// </remarks>
        public int Size => InternalStream.Size;

        /// <summary>
        /// Gets the name of the underlying symbol source.
        /// </summary>
        /// <remarks>
        /// Gets the name of the underlying symbol source. This method returns a non-null, non-empty
        /// string. If such a name is not known, this method returns
        /// <see cref="F:Antlr4.Runtime.IntStreamConstants.UnknownSourceName"/> .
        /// </remarks>
        public string SourceName => InternalStream.SourceName;

        /// <summary>
        /// The internal stream
        /// </summary>
        private ICharStream InternalStream { get; set; }

        /// <summary>
        /// Consumes the current symbol in the stream.
        /// </summary>
        /// <remarks>
        /// Consumes the current symbol in the stream. This method has the following effects:
        /// <ul><li><strong>Forward movement:</strong> The value of
        /// <see cref="P:Antlr4.Runtime.IIntStream.Index">index()</see> before calling this method is
        /// less than the value of <c>index()</c> after calling this method.</li><li><strong>Ordered
        /// lookahead:</strong> The value of <c>LA(1)</c> before calling this method becomes the
        /// value of <c>LA(-1)</c> after calling this method.</li></ul> Note that calling this method
        /// does not guarantee that <c>index()</c> is incremented by exactly 1, as that would
        /// preclude the ability to implement filtering streams (e.g.
        /// <see cref="T:Antlr4.Runtime.CommonTokenStream"/> which distinguishes between "on-channel"
        /// and "off-channel" tokens).
        /// </remarks>
        public void Consume()
        {
            InternalStream.Consume();
        }

        /// <summary>
        /// This method returns the text for a range of characters within this input stream.
        /// </summary>
        /// <param name="interval">an interval within the stream</param>
        /// <returns>the text of the specified interval</returns>
        /// <remarks>
        /// This method returns the text for a range of characters within this input stream. This
        /// method is guaranteed to not throw an exception if the specified
        /// <paramref name="interval"/> lies entirely within a marked range. For more information
        /// about marked ranges, see <see cref="M:Antlr4.Runtime.IIntStream.Mark"/> .
        /// </remarks>
        [return: NotNull]
        public string GetText(Interval interval)
        {
            return InternalStream.GetText(interval);
        }

        /// <summary>
        /// Gets the value of the symbol at offset <paramref name="i"/> from the current position.
        /// When <c>i==1</c> , this method returns the value of the current symbol in the stream
        /// (which is the next symbol to be consumed). When <c>i==-1</c> , this method returns the
        /// value of the previously read symbol in the stream. It is not valid to call this method
        /// with <c>i==0</c> , but the specific behavior is unspecified because this method is
        /// frequently called from performance-critical code.
        /// <p>This method is guaranteed to succeed if any of the following are true:</p>
        /// <ul><li><c>i&gt;0</c></li><li><c>i==-1</c> and
        /// <see cref="P:Antlr4.Runtime.IIntStream.Index">index()</see> returns a value greater than
        /// the value of <c>index()</c> after the stream was constructed and <c>LA(1)</c> was called
        /// in that order. Specifying the current <c>index()</c> relative to the index after the
        /// stream was created allows for filtering implementations that do not return every symbol
        /// from the underlying source. Specifying the call to <c>LA(1)</c> allows for lazily
        /// initialized streams.</li><li><c>LA(i)</c> refers to a symbol consumed within a marked
        /// region that has not yet been released.</li></ul>
        /// <p>
        /// If <paramref name="i"/> represents a position at or beyond the end of the stream, this
        /// method returns <see cref="F:Antlr4.Runtime.IntStreamConstants.EOF"/> .
        /// </p>
        /// <p>
        /// The return value is unspecified if <c>i&lt;0</c> and fewer than <c>-i</c> calls to
        /// <see cref="M:Antlr4.Runtime.IIntStream.Consume">consume()</see> have occurred from the
        /// beginning of the stream before calling this method.
        /// </p>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int LA(int i)
        {
            int c = InternalStream.LA(i);

            return c <= 0 ? c : (int)char.ToUpperInvariant((char)c);
        }

        /// <summary>
        /// A mark provides a guarantee that
        /// <see cref="M:Antlr4.Runtime.IIntStream.Seek(System.Int32)">seek()</see> operations will
        /// be valid over a "marked range" extending from the index where <c>mark()</c> was called to
        /// the current <see cref="P:Antlr4.Runtime.IIntStream.Index">index()</see> . This allows the
        /// use of streaming input sources by specifying the minimum buffering requirements to
        /// support arbitrary lookahead during prediction.
        /// <p>
        /// The returned mark is an opaque handle (type <c>int</c> ) which is passed to
        /// <see cref="M:Antlr4.Runtime.IIntStream.Release(System.Int32)">release()</see> when the
        /// guarantees provided by the marked range are no longer necessary. When calls to
        /// <c>mark()</c> / <c>release()</c> are nested, the marks must be released in reverse order
        /// of which they were obtained. Since marked regions are used during performance-critical
        /// sections of prediction, the specific behavior of invalid usage is unspecified (i.e. a
        /// mark is not released, or a mark is released twice, or marks are not released in reverse
        /// order from which they were created).
        /// </p>
        /// <p>
        /// The behavior of this method is unspecified if no call to an
        /// <see cref="T:Antlr4.Runtime.IIntStream">initializing method</see> has occurred after this
        /// stream was constructed.
        /// </p>
        /// <p>This method does not change the current position in the input stream.</p>
        /// <p>
        /// The following example shows the use of
        /// <see cref="M:Antlr4.Runtime.IIntStream.Mark">mark()</see> ,
        /// <see cref="M:Antlr4.Runtime.IIntStream.Release(System.Int32)">release(mark)</see> ,
        /// <see cref="P:Antlr4.Runtime.IIntStream.Index">index()</see> , and
        /// <see cref="M:Antlr4.Runtime.IIntStream.Seek(System.Int32)">seek(index)</see> as part of
        /// an operation to safely work within a marked region, then restore the stream position to
        /// its original value and release the mark.
        /// </p>
        /// <pre>IntStream stream = ...; int index = -1; int mark = stream.mark(); try { index =
        /// stream.index(); // perform work here... } finally { if (index != -1) {
        /// stream.seek(index); } stream.release(mark); }</pre>
        /// </summary>
        /// <returns>
        /// An opaque marker which should be passed to
        /// <see cref="M:Antlr4.Runtime.IIntStream.Release(System.Int32)">release()</see> when the
        /// marked range is no longer required.
        /// </returns>
        public int Mark()
        {
            return InternalStream.Mark();
        }

        /// <summary>
        /// This method releases a marked range created by a call to
        /// <see cref="M:Antlr4.Runtime.IIntStream.Mark">mark()</see> . Calls to <c>release()</c>
        /// must appear in the reverse order of the corresponding calls to <c>mark()</c> . If a mark
        /// is released twice, or if marks are not released in reverse order of the corresponding
        /// calls to <c>mark()</c> , the behavior is unspecified.
        /// <p>
        /// For more information and an example, see <see cref="M:Antlr4.Runtime.IIntStream.Mark"/> .
        /// </p>
        /// </summary>
        /// <param name="marker">A marker returned by a call to <c>mark()</c> .</param>
        /// <seealso cref="M:Antlr4.Runtime.IIntStream.Mark"/>
        public void Release(int marker)
        {
            InternalStream.Release(marker);
        }

        /// <summary>
        /// Set the input cursor to the position indicated by <paramref name="index"/> . If the
        /// specified index lies past the end of the stream, the operation behaves as though
        /// <paramref name="index"/> was the index of the EOF symbol. After this method returns
        /// without throwing an exception, the at least one of the following will be true.
        /// <ul><li><see cref="P:Antlr4.Runtime.IIntStream.Index">index()</see> will return the index
        /// of the first symbol appearing at or after the specified <paramref name="index"/> .
        /// Specifically, implementations which filter their sources should automatically adjust
        /// <paramref name="index"/> forward the minimum amount required for the operation to target
        /// a non-ignored symbol.</li><li><c>LA(1)</c> returns
        /// <see cref="F:Antlr4.Runtime.IntStreamConstants.EOF"/></li></ul> This operation is
        /// guaranteed to not throw an exception if <paramref name="index"/> lies within a marked
        /// region. For more information on marked regions, see
        /// <see cref="M:Antlr4.Runtime.IIntStream.Mark"/> . The behavior of this method is
        /// unspecified if no call to an <see cref="T:Antlr4.Runtime.IIntStream">initializing
        /// method</see> has occurred after this stream was constructed.
        /// </summary>
        /// <param name="index">The absolute index to seek to.</param>
        public void Seek(int index)
        {
            InternalStream.Seek(index);
        }
    }
}