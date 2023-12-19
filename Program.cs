//public void Book_Date_FalseReturned()
//{
//    //Arrange
//    var date = new DateTime(2023, 12, 25);
//    bool expected = false;
//    //act
//    Table tb = new Table();
//    bool result = tb.Book(date);
//    //assert
//    Assert.AreEqual(result, expected);
//}




using System;
using System.Collections.Generic;

// Головний клас програми
public class TableReservationApp
{
    static void Main(string[] args)
    {
        ReservationManagerClass m = new ReservationManagerClass();
        m.AddRestaurant("A", 10);
        m.AddRestaurant("B", 5);
        m.BookTable("c", new DateTime(2023, 12, 25), 8);

        Console.WriteLine(m.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(m.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
    }
}




public class Restaurant
{
    private string Name;
    private Table[] Tables;
    public string name { get; set; }
    public Table[] tables { get; set; }
}


public class Table
{
    private List<DateTime> bookedDate;


    public Table()
    {
        bookedDate = new List<DateTime>();
    }

    // Метод для бронювання 
    public bool Book(DateTime date)
    {
        try
        {
            if (IsBooked(date))
            {
                return false;
            }
            //Додавання запису в БД
            bookedDate.Add(date);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    // Перевірка чи заброньовано в певний час
    public bool IsBooked(DateTime date)
    {
        return bookedDate.Contains(date);
    }
}



// Reservation Manager Class
public class ReservationManagerClass
{
   
    private List<Restaurant> curretnRes;

    public ReservationManagerClass()
    {
        curretnRes = new List<Restaurant>();
    }

    // Метод для додавання ресторану
    public void AddRestaurant(string name, int table)
    {
        try
        {
            Restaurant res = new Restaurant();
            res.name = name;
            res.tables = new Table[table];
            for (int i = 0; i < table; i++)
            {
                res.tables[i] = new Table();
            }
            curretnRes.Add(res);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Метод для завантаження даних з файлу
    private void LoadResFromFile(string fileP)
    {
        try
        {
            string[] source = File.ReadAllLines(fileP);
            foreach (string item in source)
            {
                var parts = item.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                {
                    AddRestaurant(parts[0], tableCount);
                }
                else
                {
                    Console.WriteLine(item);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Метод для знаходження всіх вільних столиків
    public List<string> FreeTables(DateTime date)
    {
        try
        { 
            List<string> free = new List<string>();
            foreach (var item in curretnRes)
            {
                for (int i = 0; i < item.tables.Length; i++)
                {
                    if (!item.tables[i].IsBooked(date))
                    {
                        free.Add($"{item.name} - Table {i + 1}");
                    }
                }
            }
            return free;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return new List<string>();
        }
    }
    //Метод для перевірки бронювання столику
    public bool BookTable(string resName, DateTime date, int tableNumber)
    {
        try
        {
            foreach (var item in curretnRes)
            {
                if (item.name == resName)
                {
                    // перевірка номеру столику
                    if (tableNumber < 0 || tableNumber >= item.tables.Length)
                    {
                        Console.WriteLine("Error number of table!");
                        return false;
                    }

                    return item.tables[tableNumber].Book(date);
                }
            }
            Console.WriteLine("Uncorrect name of restauran");
            return false;
        }
        catch (Exception ex) 
        {
            Console.WriteLine("Error: " + ex.Message);
            return false;

        }
    }


    // метод для сортування по доступності
    public void SortRes(DateTime date)
    {
        try
        { 
            bool isSwapped;
            do
            {
                isSwapped = false;
                for (int i = 0; i < curretnRes.Count - 1; i++)
                {
                    // Поточний доступний стіл
                    var currentAvTable = CountAvTable(curretnRes[i], date);
                    // Наступний доступний стіл
                    var nextAvTable = CountAvTable(curretnRes[i + 1], date); 

                    if (currentAvTable < nextAvTable)
                    {
                        // Зміна ресторану
                        var temp = curretnRes[i];
                        curretnRes[i] = curretnRes[i + 1];
                        curretnRes[i + 1] = temp;
                        isSwapped = true;
                    }
                }
            } while (isSwapped);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Метод для підрахунку доступних столів в ресторані за вказаний час
    public int CountAvTable(Restaurant res, DateTime date)
    {
        try
        {
            int count = 0;
            foreach (var item in res.tables)
            {
                if (!item.IsBooked(date))
                {
                    count++;
                }
            }
            return count;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return 0;
        }
    }
}

