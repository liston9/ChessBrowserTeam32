namespace ChessBrowser;

static class PgnParser
{
    public static List<ChessGame> pgnReader(string[] PGNFileLines)
    {
        List<ChessGame> games = new();
 
        int currentLineNum = 0;
        string currentLine = PGNFileLines[0];
        while (currentLineNum < PGNFileLines.Length - 1)
        {
            Dictionary<string, string> properties = new();

            for (int i = 0; i < 11; i++)
            {
                int index = currentLine.IndexOf(' ');
                string property = currentLine.Substring(1, index - 1);
                string contents = currentLine.Substring(index + 2, currentLine.Length - index - 4);
                properties.Add(property, contents);
                currentLineNum++;
                currentLine = PGNFileLines[currentLineNum];
            }

            currentLineNum++;
            currentLine = PGNFileLines[currentLineNum];
            string moves = "";
            while (currentLine != "")
            {
                moves += currentLine;
                currentLineNum++;
                currentLine = PGNFileLines[currentLineNum];
            }

            currentLineNum++;
            currentLine = PGNFileLines[currentLineNum];

            properties.Add("Moves", moves);
            ChessGame game = new ChessGame(properties);
            games.Add(game);
        }
        return games;
    }
}