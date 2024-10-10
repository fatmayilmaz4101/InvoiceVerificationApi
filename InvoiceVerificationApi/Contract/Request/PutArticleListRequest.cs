using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.Contract.Request
{
    public class PutArticleListRequest
    {
        public string? ArticleNo { get; set; }
        public string? ArticleName { get; set; }
        public Unit? Unit { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public double? Cost { get; set; }
        public string? Description { get; set; }

    }
}