using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;

namespace DialogueEditor.Views.Common;

public class TargetedGridSplitter : GridSplitter, IDisposable
{
    //private List<ColumnDefinition> _lockedColumns = new();
    //private Dictionary<int, (GridLength, GridUnitType)> _lockedColumns = new();
    //private Dictionary<int, (GridLength, GridUnitType)> _workingColums = new();
    //private Dictionary<int, GridUnitType> _lockedColumns = new();

    ColumnDefinition? col1;
    ColumnDefinition? col2;

    public static readonly DependencyProperty FirstTargetColumnProperty =
        DependencyProperty.Register(
            "FirstTargetColumn",
            typeof(int),
            typeof(TargetedGridSplitter),
            new PropertyMetadata(0));

    public static readonly DependencyProperty SecondTargetColumnProperty =
        DependencyProperty.Register(
            "SecondTargetColumn",
            typeof(int),
            typeof(TargetedGridSplitter),
            new PropertyMetadata(1));

    public static readonly DependencyProperty MinColumnWidthProperty =
        DependencyProperty.Register(
            "MinColumnWidth",
            typeof(int),
            typeof(TargetedGridSplitter),
            new PropertyMetadata(0));

    public int FirstTargetColumn
    {
        get => (int)GetValue(FirstTargetColumnProperty);
        set => SetValue(FirstTargetColumnProperty, value);
    }

    public int SecondTargetColumn
    {
        get => (int)GetValue(SecondTargetColumnProperty);
        set => SetValue(SecondTargetColumnProperty, value);
    }

    public int MinColumnWidth
    {
        get => (int)GetValue(MinColumnWidthProperty);
        set => SetValue(MinColumnWidthProperty, value);
    }

    public TargetedGridSplitter()
    {
        this.DragStarted += OnDragStarted;
        this.DragDelta += OnDragDelta;
        this.DragCompleted += OnDragCompleted;
    }

    private void OnDragStarted(object sender, DragStartedEventArgs e)
    {
        if (Parent is Grid grid)
        {
            col1 = grid.ColumnDefinitions[FirstTargetColumn];
            col2 = grid.ColumnDefinitions[SecondTargetColumn];
        }
    }

    private void OnDragDelta(object sender, DragDeltaEventArgs e)
    {
        if (col1 is null || col2 is null)
            return;

        double delta = e.HorizontalChange;

        // Calculate new widths
        double newWidthCol1 = col1.ActualWidth + delta;
        double newWidthCol2 = col2.ActualWidth - delta;

        // Enforce minimum widths

        if (newWidthCol1 < MinColumnWidth)
        {
            newWidthCol1 = MinColumnWidth;
            newWidthCol2 = col2.ActualWidth - MinColumnWidth - col1.ActualWidth;
        }
        else if (newWidthCol2 < MinColumnWidth)
        {
            newWidthCol1 = col1.ActualWidth + col2.ActualWidth - MinColumnWidth;
            newWidthCol2 = MinColumnWidth;
        }

        // Apply constrained widths
        col1.Width = new GridLength(newWidthCol1, GridUnitType.Pixel);
        col2.Width = new GridLength(newWidthCol2, GridUnitType.Pixel);

        // Lock other columns
        //foreach (var kvp in _lockedColumns)
        //{
        //    columns[kvp.Key].Width = kvp.Value.Item1;
        //}
    }

    private void OnDragCompleted(object sender, DragCompletedEventArgs e)
    {
        //_lockedColumns.Clear(); // Clean up

    }


    void IDisposable.Dispose()
    {
        this.DragStarted -= OnDragStarted;
        this.DragDelta -= OnDragDelta;
        this.DragCompleted -= OnDragCompleted; 
    }
}