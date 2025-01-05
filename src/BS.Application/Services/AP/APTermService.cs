using BS.Domain.Exceptions;

namespace BS.Application.Services.AP;

public class APTermService : IAPTermService
{
    readonly IAPTermRepository termRepository;

    public APTermService(IAPTermRepository termRepository)
    {
        this.termRepository = termRepository;
    }

    public async Task<IEnumerable<APTerm>> GetTerms(int profileId)
    {
        return await termRepository.GetTerms(profileId, new string[] { "" });
    }


    public async Task<APTerm> GetTerm(int termId)
    {
        var term = termId == 0 ? new APTerm() : await termRepository.Get(termId);
        return term;
    }


    public async Task<ResultVM> SaveTerm(APTerm term)
    {
        bool codeAlreadyExists = await termRepository.CheckTermCodeAlreadyExist(term);
        if (codeAlreadyExists)
            return new ResultVM() { Messages = new string[] { $"term code: {term.Code} already exists for another term" } };

        if (term.ID <= 0)
        {
            term.DateCreated = term.DateModified = DateTime.Now;
            await termRepository.AddAsync(term);
            return new ResultVM() { IsSuccess = true, Messages = new string[] { $"new term {term.Code} added successfully" } };
        }

        var _ = await termRepository.Get(term.ID);
        if (_ == null)
            return new ResultVM() { IsSuccess = false, Messages = new string[] { "selected term id does not exist" } };

        if (_.CompanyID != term.CompanyID)
            return new ResultVM() { IsSuccess = false, Messages = new string[] { "incorrect company id" } };


        _.Code = term.Code;
        _.Title = term.Title;
        _.StatusID = term.StatusID;
        _.TermDays = term.TermDays;
        _.DateModified = DateTime.Now;
        await termRepository.UpdateAsync(_);
        return new ResultVM() { IsSuccess = true, Messages = new string[] { "term information updated successfully" } };
    }


    public async Task<bool> DeleteTerm(int profileId, int termId)
    {
        var term = await termRepository.Get(termId);

        if (term == null || term.CompanyID != profileId)
            throw new BSApplicationException("invalid term id");

        await termRepository.DeleteAsync(term);
        return true;
    }
}
