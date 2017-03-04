using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RecepiesEntities context = new RecepiesEntities())
            {
                //insert receipe
                /*
                Receipe receipe = new Receipe { Name = "Chicken Salad" };
                context.Receipes.Add(receipe);
                context.SaveChanges();
                */

                //select receipe
                /*
                Receipe reciepe = context.Receipes.FirstOrDefault(r => r.Name == "Chicken Salad");
                Console.WriteLine(reciepe.Id);
                */

                //update receipe
                /*
                Receipe reciepe = context.Receipes.FirstOrDefault(r => r.Name == "Chicken Salad");
                reciepe.Name = "Burger";
                context.SaveChanges();
                */

                //delete receipe
                /*
                Receipe reciepe = context.Receipes.FirstOrDefault(r => r.Name == "Chicken Salad");
                context.Receipes.Remove(reciepe);
                context.SaveChanges();
                */

                //insert category
                /*
                context.Categories.Add(new Category { Name = "Breakfast" });
                context.Categories.Add(new Category { Name = "Lunch" });
                context.SaveChanges();
                */

                //1. Using Id properties
                /*
                Category category = context.Categories.FirstOrDefault(c => c.Name == "Breakfast");
                context.Receipes.Add(new Receipe { Name = "Cereal", CategoryId = category.Id });
                context.SaveChanges();
                */

                //2. Receipe.Category navigation property
                /*                
                Category category = context.Categories.FirstOrDefault(c => c.Name == "Lunch");
                context.Receipes.Add(new Receipe { Name = "Pizza", Category = category });
                context.SaveChanges();
                */

                //3. Category.Receipies navigation property
                /*          
                Category category = context.Categories.FirstOrDefault(c => c.Name == "Lunch");
                category.Receipes.Add(new Receipe { Name = "Soup" });
                context.SaveChanges();
                */

                Category category = context.Categories.FirstOrDefault(c => c.Name == "Lunch");
                List<Receipe> recepies = category.Receipes.ToList();
                recepies.ForEach(r => Console.WriteLine(r.Name));





            }

        }
    }
}
