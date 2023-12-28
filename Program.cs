using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Theatre
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public List<Movie> Movies { get; set; } = new List<Movie>();
}

class Movie
{
    public string Name { get; set; }
    public List<Show> Shows { get; set; } = new List<Show>();
}

class Show
{
    public string Timing { get; set; }
    public decimal TotalAmountSold { get; set; }
}

class Ticket
{
    public int TicketNumber { get; set; }
    public int SeatNumber { get; set; }
    public string TheatreName { get; set; }
    public string MovieName { get; set; }
    public string ShowTiming { get; set; }
    public string TicketType { get; set; }
    public decimal Price { get; set; }
}

class Program
{
    static List<Theatre> theatres = new List<Theatre>();
    static List<Ticket> tickets = new List<Ticket>();

    static void Main()
    {
        InitializeData();

        while (true)
        {
            Console.WriteLine("1. Issue Ticket");
            Console.WriteLine("2. List Movies in a Theatre");
            Console.WriteLine("3. List All Movies");
            Console.WriteLine("4. List Theatres with Shows for a Movie");
            Console.WriteLine("5. Generate Revenue Report");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        IssueTicket();
                        break;
                    case 2:
                        ListMoviesInTheatre();
                        break;
                    case 3:
                        ListAllMovies();
                        break;
                    case 4:
                        ListTheatresWithShowsForMovie();
                        break;
                    case 5:
                        GenerateRevenueReport();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.WriteLine();
        }
    }

    static void InitializeData()
    {
        Theatre theatreA = new Theatre { Name = "A", Capacity = 50 };
        Theatre theatreB = new Theatre { Name = "B", Capacity = 25 };

        Movie movie1 = new Movie { Name = "KGF" };
        movie1.Shows.Add(new Show { Timing = "9:30 AM" });
        movie1.Shows.Add(new Show { Timing = "1:30 PM" });
        movie1.Shows.Add(new Show { Timing = "6:00 PM" });

        Movie movie2 = new Movie { Name = "KGF2" };
        movie2.Shows.Add(new Show { Timing = "10:00 AM" });
        movie2.Shows.Add(new Show { Timing = "2:00 PM" });
        movie2.Shows.Add(new Show { Timing = "7:00 PM" });

        Movie movie3 = new Movie { Name = "Leo" };
        movie1.Shows.Add(new Show { Timing = "9:30 AM" });
        movie1.Shows.Add(new Show { Timing = "1:30 PM" });
        movie1.Shows.Add(new Show { Timing = "6:00 PM" });

        Movie movie4 = new Movie { Name = "Joker" };
        movie2.Shows.Add(new Show { Timing = "10:00 AM" });
        movie2.Shows.Add(new Show { Timing = "2:00 PM" });
        movie2.Shows.Add(new Show { Timing = "7:00 PM" });

        theatreA.Movies.Add(movie1);
        theatreA.Movies.Add(movie2);

        theatreB.Movies.Add(movie3);
        theatreB.Movies.Add(movie4);

        theatres.Add(theatreA);
        theatres.Add(theatreB);
    }

    static void IssueTicket()
    {
        Console.Write("Enter Theatre Name: ");
        string theatreName = Console.ReadLine();

        Theatre theatre = theatres.Find(t => t.Name == theatreName);
        if (theatre == null)
        {
            Console.WriteLine("Invalid Theatre Name.");
            return;
        }

        Console.Write("Enter Movie Name: ");
        string movieName = Console.ReadLine();

        Movie movie = theatre.Movies.Find(m => m.Name == movieName);
        if (movie == null)
        {
            Console.WriteLine("Invalid Movie Name.");
            return;
        }

        Console.Write("Enter Show Timing: ");
        string showTiming = Console.ReadLine();

        Show show = movie.Shows.Find(s => s.Timing == showTiming);
        if (show == null)
        {
            Console.WriteLine("Invalid Show Timing.");
            return;
        }

        Console.Write("Enter Ticket Type (I, II, III): ");
        string ticketType = Console.ReadLine();

        Console.Write("Enter Ticket Price: ");
        decimal price;
        if (!decimal.TryParse(Console.ReadLine(), out price))
        {
            Console.WriteLine("Invalid Price.");
            return;
        }

        Ticket ticket = new Ticket
        {
            TicketNumber = tickets.Count + 1,
            TheatreName = theatreName,
            MovieName = movieName,
            ShowTiming = showTiming,
            TicketType = ticketType,
            Price = price
        };

        tickets.Add(ticket);
        show.TotalAmountSold += price;

        Console.WriteLine($"Ticket Issued: {ticket.TicketNumber}");
    }

    static void ListMoviesInTheatre()
    {
        Console.Write("Enter Theatre Name: ");
        string theatreName = Console.ReadLine();

        Theatre theatre = theatres.Find(t => t.Name == theatreName);
        if (theatre == null)
        {
            Console.WriteLine("Invalid Theatre Name.");
            return;
        }

        Console.WriteLine($"Movies in {theatreName}:");

        foreach (var movie in theatre.Movies)
        {
            Console.WriteLine($"- {movie.Name}");
        }
    }

    static void ListAllMovies()
    {
        Console.WriteLine("All Movies:");

        foreach (var theatre in theatres)
        {
            foreach (var movie in theatre.Movies)
            {
                Console.WriteLine($"- {movie.Name} ({theatre.Name})");
            }
        }
    }

    static void ListTheatresWithShowsForMovie()
    {
        Console.Write("Enter Movie Name: ");
        string movieName = Console.ReadLine();

        Console.WriteLine($"Theatres with Shows for {movieName}:");

        foreach (var theatre in theatres)
        {
            foreach (var movie in theatre.Movies)
            {
                if (movie.Name == movieName)
                {
                    Console.WriteLine($"- {theatre.Name}: {string.Join(", ", movie.Shows.Select(s => s.Timing))}");
                    break;
                }
            }
        }
    }

    static void GenerateRevenueReport()
    {
        Console.Write("Enter Movie Name: ");
        string movieName = Console.ReadLine();

        Console.WriteLine($"Revenue Report for {movieName}:");

        foreach (var theatre in theatres)
        {
            foreach (var movie in theatre.Movies)
            {
                if (movie.Name == movieName)
                {
                    foreach (var show in movie.Shows)
                    {
                        Console.WriteLine($"{theatre.Name}, {show.Timing}, {show.TotalAmountSold} Rs");
                    }
                    break;
                }
            }
        }
    }
}