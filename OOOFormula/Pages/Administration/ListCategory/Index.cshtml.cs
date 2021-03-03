﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListCategory
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Category> Category { get; set; }

        public SortState? CurrentSort { get; set; }

        public async Task OnGetAsync(SortState? sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder; //сохранение состояния сортировки

            IQueryable<Category> CategoryIQ = from s in _context.Category
                                              select s; //получаем записи из БД

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            CategoryIQ = Sorting(sortOrder, CategoryIQ); //сортировка

            int pageSize = 2; //количество элементов на странице
            Category = await PaginatedList<Category>.CreateAsync(
                CategoryIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }

        private static IQueryable<Category> Sorting(SortState? sortOrder, IQueryable<Category> CategoryIQ)
        {
            CategoryIQ = sortOrder switch
            {
                SortState.NameAsc => CategoryIQ.OrderBy(p => p.Name),
                SortState.NameDesc => CategoryIQ.OrderByDescending(p => p.Name),
                _ => CategoryIQ.OrderBy(p => p.Id),
            };
            return CategoryIQ;
        }
    }
}
