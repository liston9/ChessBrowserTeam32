using System.Collections;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace ChessBrowser.Components.Pages
{
  public partial class ChessBrowser
  {
    /// <summary>
    /// Bound to the Unsername form input
    /// </summary>
    private string Username = "";

    /// <summary>
    /// Bound to the Password form input
    /// </summary>
    private string Password = "";

    /// <summary>
    /// Bound to the Database form input
    /// </summary>
    private string Database = "Team32Chess";

    /// <summary>
    /// Represents the progress percentage of the current
    /// upload operation. Update this value to update 
    /// the progress bar.
    /// </summary>
    private int    Progress = 0;

    /// <summary>
    /// This method runs when a PGN file is selected for upload.
    /// Given a list of lines from the selected file, parses the 
    /// PGN data, and uploads each chess game to the user's database.
    /// </summary>
    /// <param name="PGNFileLines">The lines from the selected file</param>
    private async Task InsertGameData(string[] PGNFileLines)
    {
      // This will build a connection string to your user's database on atr,
      // assuimg you've filled in the credentials in the GUI
      string connection = GetConnectionString();

      // TODO:
      //   Parse the provided PGN data
      //   We recommend creating separate libraries to represent chess data and load the file

      List<ChessGame> games = PgnParser.pgnReader(PGNFileLines);
      
      //For debugging
      //TODO delete
      // using (StreamWriter writer = new StreamWriter("output.txt"))
      // {
      //   foreach (ChessGame game in games)
      //   {
      //     writer.WriteLine(game);
      //     writer.WriteLine("\n============   **NEW GAME**   ============\n");
      //   }
      // }

      using (MySqlConnection conn = new MySqlConnection(connection))
      {
        try
        {
          conn.Open();
          // MySqlCommand gamesCommand = conn.CreateCommand();
          // MySqlCommand whitePlayerCommand = conn.CreateCommand();
          // MySqlCommand blackPlayerCommand = conn.CreateCommand();
          // MySqlCommand eventsCommand = conn.CreateCommand();
          MySqlCommand command = conn.CreateCommand();

          
          command.CommandText = "insert ignore into Events (Name, Site, Date) values (@Event, @Site, @EventDate);";
          command.CommandText += "insert into Players (Name, Elo) values (@WhiteName, @WhiteElo) on duplicate key update Elo=if(Elo > @WhiteElo, Elo, @WhiteElo);";
          command.CommandText += "insert into Players (Name, Elo) values (@BlackName, @BlackElo) on duplicate key update Elo=if(Elo > @BlackElo, Elo, @BlackElo);";
          command.CommandText += "insert into Games (Round, Result, Moves, BlackPlayer, WhitePlayer, eID) values(@Round, @Result, @Moves, (select p.pID from Players p where p.Name=@BlackPlayer), (select p.pID from Players p where p.Name=@WhitePlayer), (select e.eID from Events e where e.Name=@EventName and e.Site=@EventSite and e.Date=@EventDate));";
          
          int tempCounter = 0;
          foreach (ChessGame game in games)
          {
            command.Parameters.AddWithValue("@Round", game.Round);
            command.Parameters.AddWithValue("@Result", game.Result);
            command.Parameters.AddWithValue("@Moves", game.Moves);
            command.Parameters.AddWithValue("@BlackPlayer", game.BlackPlayer);
            command.Parameters.AddWithValue("@WhitePlayer", game.WhitePlayer);
            command.Parameters.AddWithValue("@EventName", game.Event);
            command.Parameters.AddWithValue("@EventDate", game.EventDate);
            command.Parameters.AddWithValue("@EventSite", game.Site);

            
            command.Parameters.AddWithValue("@WhiteName", game.WhitePlayer);
            command.Parameters.AddWithValue("@WhiteElo", game.WhiteElo);
            
            command.Parameters.AddWithValue("@BlackName", game.BlackPlayer);
            command.Parameters.AddWithValue("@BlackElo", game.BlackElo);
            
            command.Parameters.AddWithValue("@Event", game.Event);
            command.Parameters.AddWithValue("@Site", game.Site);
            // command.Parameters.AddWithValue("@EventDate", game.EventDate);
            
            // eventsCommand.ExecuteNonQuery();
            // whitePlayerCommand.ExecuteNonQuery();
            // blackPlayerCommand.ExecuteNonQuery();
            await command.ExecuteNonQueryAsync();
            
            command.Parameters.Clear();
            // whitePlayerCommand.Parameters.Clear();
            // blackPlayerCommand.Parameters.Clear();
            // eventsCommand.Parameters.Clear();
            
            tempCounter++;
            Progress = (int)((tempCounter / (double)games.Count) * 100);
            Console.WriteLine(Progress);
            await InvokeAsync(StateHasChanged);
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }

    }


    /// <summary>
    /// Queries the database for games that match all the given filters.
    /// The filters are taken from the various controls in the GUI.
    /// </summary>
    /// <param name="white">The white player, or "" if none</param>
    /// <param name="black">The black player, or "" if none</param>
    /// <param name="opening">The first move, e.g. "1.e4", or "" if none</param>
    /// <param name="winner">The winner as "W", "B", "D", or "" if none</param>
    /// <param name="useDate">true if the filter includes a date range, false otherwise</param>
    /// <param name="start">The start of the date range</param>
    /// <param name="end">The end of the date range</param>
    /// <param name="showMoves">true if the returned data should include the PGN moves</param>
    /// <returns>A string separated by newlines containing the filtered games</returns>
    private string PerformQuery(string white, string black, string opening,
      string winner, bool useDate, DateTime start, DateTime end, bool showMoves)
    {
      // This will build a connection string to your user's database on atr,
      // assuimg you've typed a user and password in the GUI
      string connection = GetConnectionString();

      // Build up this string containing the results from your query
      string parsedResult = "";

      // Use this to count the number of rows returned by your query
      // (see below return statement)
      int numRows = 0;

      using (MySqlConnection conn = new MySqlConnection(connection))
      {
        try
        {
          // Open a connection
          conn.Open();
          MySqlCommand command = conn.CreateCommand();
          command.CommandText = "select Events.Name, Site, Date, White.Name, White.Elo, Black.Name, Black.Elo, Result";
          if (showMoves)
            command.CommandText += ", Moves";
          command.CommandText += " from Games join Events on Games.eID=Events.eID " +
                                 "join Players White on White.pID=Games.WhitePlayer " +
                                 "join Players Black on Black.pID=Games.BlackPlayer where 1=1";

          if (white != "")
          {
            command.Parameters.AddWithValue("@White", white);
            command.CommandText += " and Games.WhitePlayer = (select pID from Players where Name=@White)";
          }

          if (black != "")
          {
            command.Parameters.AddWithValue("@Black", black);
            command.CommandText += " and Games.BlackPlayer = (select pID from Players where Name=@Black)";
          }

          if (opening != "")
          {
            command.Parameters.AddWithValue("@Opening", opening+"%");
            command.CommandText += " and Moves like @Opening";
          }

          if (winner != "")
          {
            command.Parameters.AddWithValue("@Winner", winner);
            command.CommandText += " and Games.Result=@Winner";
          }

          if (useDate)
          {

            //startdate and enddate in here

          }
          
          command.CommandText += ";";

          using (MySqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              parsedResult += "Event: " + reader[0] + "\n" + "Site: " + reader[1] + "\n" + "Date: " + DateOnly.FromDateTime(DateTime.Parse(reader[2].ToString()))
                              + "\n" + "White: " + reader[3] + " (" + reader[4] + ")" + "\n"
                              + "Black: " + reader[5] + " (" + reader[6] + ")" + "\n" + "Result: " + reader[7];
              if (showMoves)
                parsedResult += "\n" + "Moves: " + reader[8];
              numRows++;
              parsedResult += "\n\n";
            }
          }
          

          // TODO:
          //   Generate and execute an SQL command,
          //   then parse the results into an appropriate string and return it.
        }
        catch (Exception e)
        {
          System.Diagnostics.Debug.WriteLine(e.Message);
        }
      }

      return numRows + " results\n\n" + parsedResult;
    }


    private string GetConnectionString()
    {
      return "server=atr.eng.utah.edu;database=" + Database + ";uid=" + Username + ";password=" + Password;
    }


    /// <summary>
    /// This method will run when the file chooser is used.
    /// It loads the files contents as an array of strings,
    /// then invokes the InsertGameData method.
    /// </summary>
    /// <param name="args">The event arguments, which contains the selected file name</param>
    private async void HandleFileChooser(EventArgs args)
    {
      try
      {
        string fileContent = string.Empty;

        InputFileChangeEventArgs eventArgs = args as InputFileChangeEventArgs ?? throw new Exception("unable to get file name");
        if (eventArgs.FileCount == 1)
        {
          var file = eventArgs.File;
          if (file is null)
          {
            return;
          }

          // load the chosen file and split it into an array of strings, one per line
          using var stream = file.OpenReadStream(1000000); // max 1MB
          using var reader = new StreamReader(stream);                   
          fileContent = await reader.ReadToEndAsync();
          string[] fileLines = fileContent.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

          // insert the games, and don't wait for it to finish
          // _ = throws away the task result, since we aren't waiting for it
          _ = InsertGameData(fileLines);
        }
      }
      catch (Exception e)
      {
        Debug.WriteLine("an error occurred while loading the file..." + e);
      }
    }

  }

}
