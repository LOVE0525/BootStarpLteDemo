using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LteDemo.Models
{

    public class Pager<T> :Pager where T :class,new ()
    {
        public T Data { get; set; }
    }
    public class Pager
    {
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalItemCount { get;  set; }

        /// <summary>
        /// 当前分页数
        /// </summary>
        public int CurrentPageIndex { get;  set; }

        /// <summary>
        /// 每页显示数据行数
        /// </summary>
        public int PageSize { get;  set; }

        /// <summary>
        /// 总共包含页数
        /// </summary>
        public int TotalPages { get;  set; } = 1;


        /// <summary>
        /// 显示默认分页个数(默认10)
        /// </summary>
        public int ShowItemNum { get; set; } = 10;

    }
}