﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nUpdate.Win32;

namespace nUpdate
{
    internal static class Extensions
    {
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body, int degreeOfParallelism = 4)
        {
            return TaskEx.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(degreeOfParallelism)
                select TaskEx.Run(async () => {
                    using (partition)
                        while (partition.MoveNext())
                            await body(partition.Current);
                }));
        }
        public static string ToAdequateSizeString(this long fileSize)
        {
            var sb = new StringBuilder(20);
            NativeMethods.StrFormatByteSize(fileSize, sb, 20);
            return sb.ToString();
        }

        public static UpdateVersion ToUpdateVersion(this string versionString)
        {
            return new UpdateVersion(versionString);
        }
    }
}