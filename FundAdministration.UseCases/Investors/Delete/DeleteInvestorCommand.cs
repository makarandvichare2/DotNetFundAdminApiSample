using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.UseCases.Common;
using System;

namespace FundAdministration.UseCases.Investors.Delete;
public record DeleteInvestorCommand( Guid guid ) : ICommand<Result<bool>>;

