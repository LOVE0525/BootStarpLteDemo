using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LteDemo
{
    using Models;
    using System.Text;

    /// <summary>
    /// 对于adminLte的分页样式实现主动分页
    /// </summary>
    public class LtePager
    {

        const string _Content = "<div class=\"row\" id=\"__nancy_adminlte_paer__\" {2}><div class=\"col-sm-3\"><div class=\"dataTables_info\">{0}</div></div><div class=\"col-sm-9\"><div class=\"dataTables_paginate paging_simple_numbers\" ><ul class=\"pagination\">{1}</ul></div></div></div><script>$(function(){{$.AdminLTE.paged();}});</script>";
        private int _startPageIndex;
        private int _endPageIndex;


        /// <summary>
        /// 总数据条数
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// 当前分页数
        /// </summary>
        public int CurrentIndex { get; private set; }

        /// <summary>
        /// 每页显示数据行数
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 总共包含页数
        /// </summary>
        public int TotalPages { get; private set; } = 1;


        /// <summary>
        /// 显示默认分页个数(默认10)
        /// </summary>
        public int ShowItemNum { get; set; } = 10;


        /// <summary>
        /// 引用form的id或者class名称
        /// </summary>
        public string RefFrom { get; set; }

        /// <summary>
        /// 替换内容页
        /// </summary>
        public string ReplaceTarget { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <param name="total"></param>
        /// <param name="size"></param>
        /// <param name="showItem"></param>
        /// <param name="refFrom"></param>
        /// <param name="replaceTarget">如果不提供,则使用直接刷新本页形式</param>
        public LtePager(int currentIndex, int total, int size = 20, int showItem = 10, string refFrom = "", string replaceTarget = "")
        {
            if (size <= 0)
            {
                throw new ArgumentException("size 不能小于等于0");
            }

            this.Total = total;
            this.CurrentIndex = currentIndex;
            this.PageSize = size;
            if (showItem <= 0)
            {
                showItem = 10;
            }
            ShowItemNum = showItem;


            if (Total > 0)
            {
                TotalPages = (int)Math.Ceiling((double)Total / PageSize);
                this._startPageIndex = CurrentIndex - (ShowItemNum / 2);
                if (_startPageIndex + ShowItemNum > TotalPages)
                    _startPageIndex = (TotalPages + 1) - ShowItemNum;
                if (_startPageIndex < 1)
                    _startPageIndex = 1;

                this._endPageIndex = _startPageIndex + ShowItemNum - 1;
                if (_endPageIndex > TotalPages)
                    _endPageIndex = TotalPages;
            }
            RefFrom = refFrom;
            if (!string.IsNullOrEmpty(replaceTarget))
            {
                ReplaceTarget = "data-ajax-update='" + replaceTarget + "' data-ajax=\"true\" data-ajax-call-frist=\"$.AdminLTE.ltePagerBuilder\"";
            }
        }

        public IHtmlString Render()
        {
            var str = string.Format(_Content, GetInfo(), BuildPageIetm(), String.IsNullOrEmpty(RefFrom) ? "" : $"ref-from=\"{RefFrom}\"");
            return new HtmlString(str);
        }

        private string GetInfo()
        {
            if (CurrentIndex == 1)
            {
                return $"显示1到{(PageSize>Total?Total:PageSize)} / 共{Total}条数据";
            }
            else
            {
                var to = CurrentIndex * PageSize;
                to = (to > Total ? Total : to);
                return $"显示{((CurrentIndex - 1) * PageSize)+1}到{to} / 共{Total}条数据";
            }
        }

        private void BuildPrevious(StringBuilder sb)
        {
            if (CurrentIndex <= 1)
            {
                sb.Append("<li class=\"paginate_button previous disabled\" ><a href=\"#\" data-dt-idx=\"1\" >首页</a></li>");
                sb.Append("<li class=\"paginate_button previous disabled\" ><a href=\"#\" data-dt-idx=\"1\" >上一页</a></li>");
            }
            else
            {
                sb.Append($"<li class=\"paginate_button previous\" ><a href=\"#\" data-dt-idx=\"1\" {ReplaceTarget}>首页</a></li>");
                sb.Append($"<li class=\"paginate_button previous\" ><a href=\"#\" data-dt-idx=\"1\" {ReplaceTarget}>上一页</a></li>");
            }
        }

        private void AddPreMore(StringBuilder sb)
        {
            if (_startPageIndex > 1 && TotalPages > 1)
            {
                var index = _startPageIndex - (ShowItemNum / 2);
                if (index < 1) index = 1;
                sb.Append($"<li class=\"paginate_button previous\" ><a href=\"#\" data-dt-idx=\"{index}\" {ReplaceTarget} >...</a></li>");
            }
        }

        private void BuildLastButton(StringBuilder sb)
        {
            if (CurrentIndex >= TotalPages)
            {
                sb.Append($"<li class=\"paginate_button previous disabled\" ><a href=\"#\" data-dt-idx=\"{CurrentIndex + 1}\" >下一页</a></li>");
                sb.Append($"<li class=\"paginate_button previous disabled\" ><a href=\"#\" data-dt-idx=\"{TotalPages}\" >尾页</a></li>");
            }
            else
            {

                sb.Append($"<li class=\"paginate_button previous\" ><a href=\"#\" data-dt-idx=\"{CurrentIndex + 1}\" {ReplaceTarget}>下一页</a></li>");
                sb.Append($"<li class=\"paginate_button previous\" ><a href=\"#\" data-dt-idx=\"{TotalPages}\" {ReplaceTarget}>尾页</a></li>");
            }
        }

        private void AddMoreAfter(StringBuilder sb)
        {

            if (_endPageIndex < TotalPages && TotalPages > 1)
            {
                var index = _endPageIndex + (ShowItemNum / 2);
                if (_endPageIndex <= ShowItemNum)
                {
                    index = _endPageIndex + 1;
                }
                if (index > TotalPages) { index = TotalPages; }
                sb.Append($"<li class=\"paginate_button previous\" ><a href=\"#\" data-dt-idx=\"{index}\" {ReplaceTarget}>...</a></li>");
            }
        }
        private string BuildPageIetm()
        {

            StringBuilder sb = new StringBuilder();
            BuildPrevious(sb);
            AddPreMore(sb);
            if (Total > 0)
            {
                for (var pageIndex = _startPageIndex; pageIndex <= _endPageIndex; pageIndex++)
                {
                    if (CurrentIndex == pageIndex)
                    {
                        sb.Append($"<li class=\"paginate_button active disabled\"><a href=\"#\" data-dt-idx=\"{pageIndex}\" {ReplaceTarget}>{pageIndex}</a></li>");
                    }
                    else
                    {
                        sb.Append($"<li class=\"paginate_button\"><a href=\"#\" data-dt-idx=\"{pageIndex}\" {ReplaceTarget} >{pageIndex}</a></li>");
                    }
                }
            }
            AddMoreAfter(sb);
            BuildLastButton(sb);
            return sb.ToString();


        }


        /*
         <div class="row">
            <div class="col-sm-5">
                <div class="dataTables_info" id="example1_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div></div>
            <div class="col-sm-7">
                <div class="dataTables_paginate paging_simple_numbers" id="example1_paginate">
                    <ul class="pagination">
                        <li class="paginate_button previous disabled" id="example1_previous"><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>
                        <li class="paginate_button active"><a href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>
                        <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="2" tabindex="0">2</a></li>
                        <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="3" tabindex="0">3</a></li>
                        <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="4" tabindex="0">4</a></li>
                        <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="5" tabindex="0">5</a></li>
                        <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="6" tabindex="0">6</a></li>
                        <li class="paginate_button next" id="example1_next"><a href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>
                    </ul>
                </div>
            </div>
        </div>
         */
    }

    public static class LtePagerExt
    {
        /// <summary>
        ///渲染分页
        /// </summary>
        /// <typeparam name="T">分页内包含数据</typeparam>
        /// <param name="pager">数据对象</param>
        /// <returns>如果分页数据为null 则不返回任何渲染</returns>
        public static IHtmlString RenderPager(this Pager pager, string repcObj = "", string refFrom = "")
        {
            if (pager == null)
            {
                return new HtmlString("");
            }
            var page = new LtePager(pager.CurrentPageIndex, pager.TotalItemCount, pager.PageSize, refFrom: refFrom, replaceTarget: repcObj);
            return page.Render();
        }

  
    }
}