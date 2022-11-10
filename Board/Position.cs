namespace board {
  class Position {
    // {get; set} -> encapsulamento, indica que pode ser acessado e alterados por outras classes
    public int line {get; set;}
    public int column {get; set;}

    public Position(int line, int column)
    {
      this.line = line;
      this.column = column;
    }

    public void setValues(int line, int column) {
      this.line = line;
      this.column = column;
    }

    public override string ToString()
    {
      return line
      + ", "
      + column;
    }
    }
}