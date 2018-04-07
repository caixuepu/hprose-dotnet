﻿/**********************************************************\
|                                                          |
|                          hprose                          |
|                                                          |
| Official WebSite: http://www.hprose.com/                 |
|                   http://www.hprose.org/                 |
|                                                          |
\**********************************************************/
/**********************************************************\
 *                                                        *
 * ValueTupleSerializer.cs                                *
 *                                                        *
 * ValueTupleSerializer class for C#.                     *
 *                                                        *
 * LastModified: Apr 7, 2018                              *
 * Author: Ma Bingyao <andot@hprose.com>                  *
 *                                                        *
\**********************************************************/

using System;

using static Hprose.IO.HproseTags;

namespace Hprose.IO.Serializers {
    static class ValueTupleHelper<T> {
        public static volatile int Length;
        public static volatile Action<Writer, T> WriteElements;
        static ValueTupleHelper() {
            Type type = typeof(T);
            if (type.IsGenericType) {
                var t = type.GetGenericTypeDefinition();
                if (t.Name.StartsWith("ValueTuple`")) {
                    Type[] args = type.GetGenericArguments();
                    typeof(ValueTupleHelper).GetMethod($"Initialize{args.Length}").MakeGenericMethod(args).Invoke(null, null);
                    return;
                }
            }
            WriteElements = Serializer<T>.Instance.Serialize;
            Length = 1;
        }
    }

    static class ValueTupleHelper {
        public static void Initialize1<T1>() {
            ValueTupleHelper<ValueTuple<T1>>.Length = 1;
            ValueTupleHelper<ValueTuple<T1>>.WriteElements = (writer, obj) => {
                Serializer<T1>.Instance.Serialize(writer, obj.Item1);
            };
        }
        public static void Initialize2<T1, T2>() {
            ValueTupleHelper<ValueTuple<T1, T2>>.Length = 2;
            ValueTupleHelper<ValueTuple<T1, T2>>.WriteElements = (writer, obj) => {
                Serializer<T1>.Instance.Serialize(writer, obj.Item1);
                Serializer<T2>.Instance.Serialize(writer, obj.Item2);
            };
        }
        public static void Initialize3<T1, T2, T3>() {
            ValueTupleHelper<ValueTuple<T1, T2, T3>>.Length = 3;
            ValueTupleHelper<ValueTuple<T1, T2, T3>>.WriteElements = (writer, obj) => {
                Serializer<T1>.Instance.Serialize(writer, obj.Item1);
                Serializer<T2>.Instance.Serialize(writer, obj.Item2);
                Serializer<T3>.Instance.Serialize(writer, obj.Item3);
            };
        }
        public static void Initialize4<T1, T2, T3, T4>() {
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4>>.Length = 4;
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4>>.WriteElements = (writer, obj) => {
                Serializer<T1>.Instance.Serialize(writer, obj.Item1);
                Serializer<T2>.Instance.Serialize(writer, obj.Item2);
                Serializer<T3>.Instance.Serialize(writer, obj.Item3);
                Serializer<T4>.Instance.Serialize(writer, obj.Item4);
            };
        }
        public static void Initialize5<T1, T2, T3, T4, T5>() {
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4, T5>>.Length = 5;
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4, T5>>.WriteElements = (writer, obj) => {
                Serializer<T1>.Instance.Serialize(writer, obj.Item1);
                Serializer<T2>.Instance.Serialize(writer, obj.Item2);
                Serializer<T3>.Instance.Serialize(writer, obj.Item3);
                Serializer<T4>.Instance.Serialize(writer, obj.Item4);
                Serializer<T5>.Instance.Serialize(writer, obj.Item5);
            };
        }
        public static void Initialize6<T1, T2, T3, T4, T5, T6>() {
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4, T5, T6>>.Length = 6;
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4, T5, T6>>.WriteElements = (writer, obj) => {
                Serializer<T1>.Instance.Serialize(writer, obj.Item1);
                Serializer<T2>.Instance.Serialize(writer, obj.Item2);
                Serializer<T3>.Instance.Serialize(writer, obj.Item3);
                Serializer<T4>.Instance.Serialize(writer, obj.Item4);
                Serializer<T5>.Instance.Serialize(writer, obj.Item5);
                Serializer<T6>.Instance.Serialize(writer, obj.Item6);
            };
        }
        public static void Initialize7<T1, T2, T3, T4, T5, T6, T7>() {
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>.Length = 7;
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>.WriteElements = (writer, obj) => {
                Serializer<T1>.Instance.Serialize(writer, obj.Item1);
                Serializer<T2>.Instance.Serialize(writer, obj.Item2);
                Serializer<T3>.Instance.Serialize(writer, obj.Item3);
                Serializer<T4>.Instance.Serialize(writer, obj.Item4);
                Serializer<T5>.Instance.Serialize(writer, obj.Item5);
                Serializer<T6>.Instance.Serialize(writer, obj.Item6);
                Serializer<T7>.Instance.Serialize(writer, obj.Item7);
            };
        }
        public static void Initialize8<T1, T2, T3, T4, T5, T6, T7, TRest>() where TRest : struct {
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>.Length = 7 + ValueTupleHelper<TRest>.Length;
            ValueTupleHelper<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>.WriteElements = (writer, obj) => {
                Serializer<T1>.Instance.Serialize(writer, obj.Item1);
                Serializer<T2>.Instance.Serialize(writer, obj.Item2);
                Serializer<T3>.Instance.Serialize(writer, obj.Item3);
                Serializer<T4>.Instance.Serialize(writer, obj.Item4);
                Serializer<T5>.Instance.Serialize(writer, obj.Item5);
                Serializer<T6>.Instance.Serialize(writer, obj.Item6);
                Serializer<T7>.Instance.Serialize(writer, obj.Item7);
                ValueTupleHelper<TRest>.WriteElements(writer, obj.Rest);
            };
        }
    }

    class ValueTupleSerializer : ReferenceSerializer<ValueTuple> {
        public override void Write(Writer writer, ValueTuple obj) {
            base.Write(writer, obj);
            var stream = writer.Stream;
            stream.WriteByte(TagList);
            stream.WriteByte(TagOpenbrace);
            stream.WriteByte(TagClosebrace);
        }
    }

    class ValueTupleSerializer<T> : ReferenceSerializer<T> {
        public override void Write(Writer writer, T obj) {
            base.Write(writer, obj);
            var stream = writer.Stream;
            stream.WriteByte(TagList);
            ValueWriter.WriteInt(stream, ValueTupleHelper<T>.Length);
            stream.WriteByte(TagOpenbrace);
            ValueTupleHelper<T>.WriteElements(writer, obj);
            stream.WriteByte(TagClosebrace);
        }
    }
}