// Copyright (c) 2018 Alachisoft
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License

using System;
using System.Web.SessionState;
using Alachisoft.NCache.IO;

namespace Alachisoft.NCache.Serialization.Surrogates
{
    /// <summary>
    /// Surrogate for <see cref="System.Object"/> objects. Also serves the
    /// purpose of default surrogate. It uses .NET native serialization
    /// </summary>
    sealed class SessionStateCollectionSerializationSurrogate : SerializationSurrogate
    {
        
        public SessionStateCollectionSerializationSurrogate(Type t) : base(t) { }

        /// <summary>
        /// Uses a <see cref="BinaryFormatter"/> to read an object of 
        /// type <see cref="ActualType"/> from the underlying stream.
        /// </summary>
        /// <param name="reader">stream reader</param>
        /// <returns>object read from the stream reader</returns>
        public override object Read(CompactBinaryReader reader)
        {
            int cookie = reader.ReadInt32();
            object custom = reader.Context.GetObject(cookie);
            if (custom == null)
            {
              
                custom = SessionStateItemCollection.Deserialize(reader.BaseReader);
                reader.Context.RememberObject(custom, false);
            }
            return custom;
        }

        /// <summary>
        /// Uses a <see cref="BinaryFormatter"/> to write an object of 
        /// type <see cref="ActualType"/> to the underlying stream
        /// </summary>
        /// <param name="writer">stream writer</param>
        /// <param name="graph">object to be written to the stream reader</param>
        public override void Write(CompactBinaryWriter writer, object graph)
        {
            int cookie = writer.Context.GetCookie(graph);
            if (cookie != SerializationContext.INVALID_COOKIE)
            {
                writer.Write(cookie);
                return;
            }

            cookie = writer.Context.RememberObject(graph, true);
            writer.Write(cookie);
            ((SessionStateItemCollection)graph).Serialize(writer.BaseWriter);
           
        }

        public override void Skip(CompactBinaryReader reader)
        {
            int cookie = reader.ReadInt32();
            object custom = reader.Context.GetObject(cookie);
            if (custom == null)
            {
              
                custom = SessionStateItemCollection.Deserialize(reader.BaseReader);
                reader.Context.RememberObject(custom, false);
            }
        }
    }
}