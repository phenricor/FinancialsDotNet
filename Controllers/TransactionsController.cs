using Financials.Dtos;
using Financials.Services;
using Financials.Models;
using Microsoft.AspNetCore.Mvc;
using Financials.Repositories;
using Financials.ViewModels;

namespace Financials.Controllers;

public class TransactionsController : Controller
{
    private readonly ITransactionRepository _repository;
    private readonly CsvImporterService _csvImport;
    private readonly TransactionMappingService _mappings;
    
    public TransactionsController(ITransactionRepository repository, CsvImporterService csvImport, TransactionMappingService mappings) {
        _repository = repository;
        _csvImport = csvImport;
        _mappings = mappings;
    }
    public IActionResult Index()
    {
        var transactions = _repository.GetTransactions()
            .Select(t => new TransactionViewModel()
            {
                Date = t.Date.ToString("yyyy-MM-dd"),
                Description = t.Description.Length > 100 ? t.Description.Substring(0, 100) : t.Description,
                Value = t.Value.ToString("c"),
                Id = t.Id,
                Tag = _repository.GetTagName(t),
                Bucket = t.BucketId.ToString(),
            }).ToList();
        return View(transactions);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Transaction obj)
    {
        if (ModelState.IsValid)
        {
            _repository.Add(obj);
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        _repository.Delete(id);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        Transaction transaction = _repository.Find(id);
        return View(transaction);
    }
    [HttpPost]
    public IActionResult Edit(Transaction obj)
    {
        if (!ModelState.IsValid) return View(obj);
        _repository.Update(obj);
        return RedirectToAction("Index");
    }
    public IActionResult Import()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Import(IFormFile file)
    {
        try
        {
            int recordsImported = _csvImport.ImportTransactions(file);
            if (recordsImported > 0)
            {
                TempData["SuccessMessage"] = $"{recordsImported} transactions imported.";
            }
            else
            {
                TempData["AlertMessage"] = $"No transactions imported.";
            }
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IActionResult MapTransactions()
    {
        try
        {
            var transactions = _repository.GetTransactions();
            var rowsAffected = _mappings.MapBucketsOnTransactions(transactions);
            if (rowsAffected > 0)
            {
                TempData["SuccessMessage"] = $"{rowsAffected} transactions mapped.";
            }
            else
            {
                TempData["AlertMessage"] = $"No transactions mapped.";
            }
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}