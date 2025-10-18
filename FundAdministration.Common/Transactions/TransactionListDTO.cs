using FundAdministration.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Common.Transactions
{
    public record TransactionListDTO(
    string investorName,
    string amount,
    string transactionDate,
    TransactionType transactionType);
}
