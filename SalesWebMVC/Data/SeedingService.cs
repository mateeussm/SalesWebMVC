using SalesWebMVC.Models;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _context;

        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Departament.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any())
            {
                return; //Db has been seeded
            }
           
            Departament d1 = new Departament(1,"Eletronics");
            Departament d2 = new Departament(2, "Notebooks");
            Departament d3 = new Departament(3, "Computer");
            Departament d4 = new Departament(4, "Books");

            Seller s1 = new Seller(1,"Mateus Silva","mateus@teste.com.br",new DateTime(1996,12,25, 0, 0, 0, DateTimeKind.Utc),4500.0,d1);
            Seller s2 = new Seller(2, "Jhones Correa", "jhones@teste.com.br", new DateTime(1998, 07, 11, 0, 0, 0, DateTimeKind.Utc), 3500.0, d2);
            Seller s3 = new Seller(3, "Sky ", "sky@teste.com.br", new DateTime(2020, 02, 25, 0, 0, 0, DateTimeKind.Utc), 2500.0, d3);
            Seller s4 = new Seller(4, "Juper ", "juper@teste.com.br", new DateTime(2020, 01, 25, 0, 0, 0, DateTimeKind.Utc), 2500.0, d4);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2024,07,28, 0, 0, 0, DateTimeKind.Utc),3850.01,Models.Enums.SaleStatus.Billed,s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2024, 07, 28, 0, 0, 0, DateTimeKind.Utc), 3850.02, Models.Enums.SaleStatus.Billed, s1);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2024, 07, 28, 0, 0, 0, DateTimeKind.Utc), 5952.51, Models.Enums.SaleStatus.Canceled, s2);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2024, 07, 28, 0, 0, 0, DateTimeKind.Utc), 2751.61, Models.Enums.SaleStatus.pending, s3);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2024, 07, 28, 0, 0, 0, DateTimeKind.Utc), 1553.21, Models.Enums.SaleStatus.Billed, s4);

            _context.Departament.AddRange(d1,d2,d3,d4);
            _context.Seller.AddRange(s1,s2,s3,s4);
            _context.SalesRecord.AddRange(r1,r2,r3,r4);

            _context.SaveChanges();
        }
    }
}