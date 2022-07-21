namespace Apt.Chess.WinUI.Renderer;

public interface IRendererSettings
{
   int SquareBoarderWidth { get;  }
   int SquareClientSizeWidth { get;  }
   int SquareFontSize { get; }

   Color WhiteSquareColor { get;  }
   Color BlackSquareColor { get;  }
   Color BorderColorInactive { get; }
   Color BorderColorMouseOver { get; }
   Color BorderColorSelectedFrom { get; }
}

public class RendererSettings : IRendererSettings
{
   public int SquareBoarderWidth { get; set; } = 6; //4;
   public int SquareClientSizeWidth { get; set; } = 90; //60; //44;
   public int SquareFontSize { get; set; } = 32; //24;
   public Color WhiteSquareColor { get; set; } = Color.Orange;
   public Color BlackSquareColor { get; set; } = Color.OrangeRed;
   public Color BorderColorInactive { get; set; } = Color.Gray;
   public Color BorderColorMouseOver { get; set; } = Color.AliceBlue;
   public Color BorderColorSelectedFrom { get; set; } = Color.GreenYellow;
}
