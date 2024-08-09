using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceVerificationApi.BusinessLogic.Entity
{
    public class CompanyPriceListEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("unit_price")]
        public double UnitPrice { get; set; }
        [Column("currency_type")]
        public required string CurrencyType { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

    }
}