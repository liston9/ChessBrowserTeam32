namespace ChessBrowser;

static class PgnParser
{
    public static List<ChessGame> pgnReader(string[] PGNFileLines)
    {
        List<ChessGame> games = new();
        
        bool newGame = false;
        int currentLineNum = 0;
        string currentLine = PGNFileLines[0];
        while (currentLineNum < PGNFileLines.Length - 1) //TODO
        {
            Dictionary<string, string> properties = new();

            for (int i = 0; i < 11; i++)
            {
                // properties.add();

            }

            currentLine = PGNFileLines[currentLineNum += 12];
            while (currentLine != "")
            {

                currentLine = PGNFileLines[currentLineNum++];
            }
            
            currentLine = PGNFileLines[currentLineNum++];
            
            //create new ChessGame and add it to List
        }
        return games;
    }
}