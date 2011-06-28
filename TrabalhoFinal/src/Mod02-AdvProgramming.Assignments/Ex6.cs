using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Mod02_AdvProgramming.Assignments
{
    public static class Ex6
    {
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,
                                                          Func<TSource, bool> predicate
                                                         )
        {
            foreach (var s in source)
            {
                if (predicate(s))
                { yield return s; }
            }
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source,
                                                                    Func<TSource, TResult> selector
                                                                    )
        {

            foreach (var s in source)
            {
                yield return selector(s);
            }

        }


        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first,
                                                            IEnumerable<TSource> second
                                                          )
        {
            //List<TSource> list = new List<TSource>();
            foreach (var s1 in first)
            {
                yield return s1;
                //list.Add(s1);
            }

            foreach (var s2 in second)
            {
                yield return s2;
                //list.Add(s2);
            }
            //return list;
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            foreach (var source1 in source)
            {
                return source1;
            }
            throw new InvalidOperationException();

        }


        public static TSource Last<TSource>(this IEnumerable<TSource> source,
                                            Func<TSource, bool> predicate
)
        {

            TSource last = default(TSource);
            foreach (var cur in source)
            {
                if (predicate(cur))
                {
                    last = cur;
                }
            }
            return last;

        }


        //Last sem predicate
        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {

            TSource last = default(TSource);
            foreach (var cur in source)
            {
                last = cur;
            }
            return last;

        }



        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source,
	                                                        int count)
        {
            int totalreturn = 0;

            foreach (var firstcountelements in source)
            {
                if (totalreturn++ == count) yield break;
                yield return firstcountelements;
            }

        }

        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source,
	                                                            Func<TSource, bool> predicate
                                                             )
        {
            
            foreach (var source1 in source)
            {
                if (!predicate(source1))
                {
                    yield break;
                }
                yield return source1;
            }
        }

        public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source,
	                                                     int count)
        {

            foreach (var source1 in source)
            {
                if (--count < 0)
                {
                    yield return source1;
                }
            }
        }

        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source,
	                                                          Func<TSource, bool> predicate)
        {
            foreach (var source1 in source)
            {
                if (predicate(source1))
                {
                    yield return source1;
                }
            }
        }


        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first,
	                                                                     IEnumerable<TSecond> second,
	                                                                     Func<TFirst, TSecond, TResult> resultSelector
                                                                        )
        {
            IEnumerator<TFirst> eFirst = first.GetEnumerator();
            IEnumerator<TSecond> eSecond = second.GetEnumerator();

            while (eFirst.MoveNext() && eSecond.MoveNext())
            {
                yield return resultSelector(eFirst.Current, eSecond.Current);
            }


        }
    

        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source,
                                                             Func<TSource, TSource, TSource> func)
        {

            TSource total = default(TSource);

            foreach (var source1 in source)
            {
                total = func(total, source1);
            }
            return total;

        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(
	                            this IEnumerable<TSource> source, 
                                Func<TSource, IEnumerable<TResult>> selector)
        {

            foreach (var s1 in source)
            {

                foreach (var result in selector(s1))
                {
                    yield return result;
                }
            }
        }

        //public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
        //                                                        this IEnumerable<TOuter> outer,
        //                                                        IEnumerable<TInner> inner,
        //                                                        Func<TOuter, TKey> outerKeySelector,
        //                                                        Func<TInner, TKey> innerKeySelector,
        //                                                        Func<TOuter, TInner, TResult> resultSelector)
        //{
            

        //}
    }
}
