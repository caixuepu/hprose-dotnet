﻿/*--------------------------------------------------------*\
|                                                          |
|                          hprose                          |
|                                                          |
| Official WebSite: https://hprose.com                     |
|                                                          |
|  DateTimeDeserializer.cs                                 |
|                                                          |
|  DateTimeDeserializer class for C#.                      |
|                                                          |
|  LastModified: Jan 11, 2019                              |
|  Author: Ma Bingyao <andot@hprose.com>                   |
|                                                          |
\*________________________________________________________*/

using System;

namespace Hprose.IO.Deserializers {
    using static Tags;

    internal class DateTimeDeserializer : Deserializer<DateTime> {
        public override DateTime Read(Reader reader, int tag) {
            var stream = reader.Stream;
            switch (tag) {
                case TagDate:
                    return ReferenceReader.ReadDateTime(reader);
                case TagTime:
                    return ReferenceReader.ReadTime(reader);
                case TagInteger:
                    return new DateTime(ValueReader.ReadInt(stream));
                case TagLong:
                    return new DateTime(ValueReader.ReadLong(stream));
                case TagDouble:
                    return new DateTime((long)ValueReader.ReadDouble(stream));
                case '0':
                case TagEmpty:
                case TagFalse:
                    return new DateTime(0);
                case '1':
                case TagTrue:
                    return new DateTime(1);
                case TagString:
                    return Converter<DateTime>.Convert(ReferenceReader.ReadString(reader));
                default:
                    if (tag >= '2' && tag <= '9') {
                        return new DateTime(tag - '0');
                    }
                    return base.Read(reader, tag);
            }
        }
    }
}
