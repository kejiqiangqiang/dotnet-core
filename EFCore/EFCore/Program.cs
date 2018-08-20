using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var db = new MyDbContext())
            {
                var users = db.User;
                if (users.Count()==0)
                {
                    //db.Users.Add(new User() {Email = "1@163.com" });
                    db.User.AddRange(new User[] { new User() { Email = "1@163.com" }, new User() { Email = "2@163.com" }, new User() { Email = "3@163.com" } });
                    var count = db.SaveChanges();
                    users = db.User;
                }
                var subs = db.Subscribe;

                //一步到位join失败--必须先ToList()
                var query0 = subs.GroupBy(p => p.Sub_Type)
                    .SelectMany(p => p.OrderByDescending(pp => pp.Ammount).Take(2).Select(pp => new { Sub_Type = p.Key, pp.Id, pp.Ammount }))
                    .ToList()//必须先ToList(),
                    .Join(users, p => p.Id, q => q.Id, (p, q) => new { p.Sub_Type, p.Id, q.Email, p.Ammount });
               
                //一步到位join失败
                var query = subs.GroupBy(p => p.Sub_Type)
                    .Select(p => p.OrderByDescending(pp => pp.Ammount).Take(2).Select(pp => new { Sub_Type = p.Key, pp.Id, pp.Ammount }))
                    .SelectMany(p=>p.Select(pp=>new { pp.Sub_Type,pp.Id,pp.Ammount}))
                    //.ToList()
                    .Join(users, p => p.Id, q => q.Id, (p, q) => new { p.Sub_Type, p.Id, q.Email, p.Ammount });

                //join之前结果正常
                var query1 = subs.GroupBy(p => p.Sub_Type)
                    .Select(p => new
                    {
                        Sub_Type = p.Key,
                        GroupsTop2 = p.OrderByDescending(pp => pp.Ammount).Take(2).Select(pp => new { Sub_Type = p.Key, pp.Id, pp.Ammount })
                    })
                    .SelectMany(p => p.GroupsTop2.Select(pp => new { pp.Sub_Type, pp.Id, pp.Ammount }));
                //.Join(users, p => p.Id, q => q.Id, (p, q) => new { p.Sub_Type, p.Id, q.Email, p.Ammount });

                //一步到位join失败--必须先ToList()
                var query2 = subs.GroupBy(p => p.Sub_Type)
                .Select(p => new
                {
                    Sub_Type = p.Key,
                    GroupsTop2 = p.OrderByDescending(pp => pp.Ammount).Take(2).Select(pp => new { Sub_Type = p.Key, pp.Id, pp.Ammount })
                })
                .SelectMany(p => p.GroupsTop2.Select(pp => new { pp.Sub_Type, pp.Id, pp.Ammount }))
                .ToList()
                .Join(users, p => p.Id, q => q.Id, (p, q) => new { p.Sub_Type, p.Id, q.Email, p.Ammount });

                //net4.5的linq to sql，不必ToList（）结果正确，并可以正确生成sql（Cross Apply）
                string sql = query.ToSql();//Microsoft.EntityFrameworkCore.Tools版本//必须与Pomelo.EntityFrameworkCore.Extensions.ToSql版本对应
            }
        }
    }
}
