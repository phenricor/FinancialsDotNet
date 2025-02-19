using Financials.Models;
using Microsoft.AspNetCore.Mvc;
using Financials.Repositories;

namespace Financials.Controllers;

public class TransactionsController : Controller
{
    private readonly ITransactionRepository _repository;
    public TransactionsController(ITransactionRepository repository) {
        _repository = repository;
    }
    public IActionResult Index()
    {
        IEnumerable<Transaction> transactions = _repository.GetTransactions();
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
}