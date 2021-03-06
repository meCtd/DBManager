﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Application.ViewModels.MetadataTree
{
    public static class TreeExtensions
    {

        public static void TreeActionTraversal<TObj>(IEnumerable<TObj> rootItems, Func<TObj, IEnumerable<TObj>> childGetter,
            Action<TObj> action)
        {
            foreach (var item in rootItems)
            {
                action(item);
                TreeActionTraversal(childGetter(item), childGetter, action);
            }
        }

        public static IEnumerable<TResult> TreeGetTraversal<TObj, TResult>(IEnumerable<TObj> rootItems, Func<TObj, IEnumerable<TObj>> childGetter,
            Func<TObj, TResult> selector)
        {
            foreach (var item in rootItems)
            {
                yield return selector(item);

                foreach (var result in TreeGetTraversal(childGetter(item), childGetter, selector))
                {
                    yield return result;
                }
            }
        }
    }
}
