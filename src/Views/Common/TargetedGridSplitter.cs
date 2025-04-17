using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;

namespace DialogueEditor.Views.Common;

public class TargetedGridSplitter : GridSplitter, IDisposable
{
    private Dictionary<int, GridLength> _lockedColumns = new();
    //private Dictionary<int, GridUnitType> _lockedColumns = new();

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
            // Store original widths of NON-target columns
            _lockedColumns = new();

            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                if (i != FirstTargetColumn && i != SecondTargetColumn)
                {
                    _lockedColumns[i] = grid.ColumnDefinitions[i].Width;
                }
            }
        }
    }

    private void OnDragDelta(object sender, DragDeltaEventArgs e)
    {
        if (Parent is not Grid grid || !_lockedColumns.Any())
            return;

        var columns = grid.ColumnDefinitions;

        if (FirstTargetColumn < 0 || FirstTargetColumn >= columns.Count ||
            SecondTargetColumn < 0 || SecondTargetColumn >= columns.Count)
            return;

        var col1 = columns[FirstTargetColumn];
        var col2 = columns[SecondTargetColumn];
        double delta = e.HorizontalChange;

        // Calculate new widths
        double newWidthCol1 = col1.ActualWidth + delta;
        double newWidthCol2 = col2.ActualWidth - delta;

        // Enforce minimum widths
        //var minWidth = MinColumnWidth * 1.0;

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
        foreach (var kvp in _lockedColumns)
        {
            columns[kvp.Key].Width = kvp.Value;
        }
    }

    private void OnDragCompleted(object sender, DragCompletedEventArgs e)
    {
        _lockedColumns.Clear(); // Clean up
    }


    void IDisposable.Dispose()
    {
        this.DragStarted -= OnDragStarted;
        this.DragDelta -= OnDragDelta;
        this.DragCompleted -= OnDragCompleted; 
    }
}