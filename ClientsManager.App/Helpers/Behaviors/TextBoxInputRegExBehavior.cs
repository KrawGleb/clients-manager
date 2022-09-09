using Microsoft.Xaml.Behaviors;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClientsManager.App.Helpers.Behaviors;

public class TextBoxInputRegExBehavior : Behavior<TextBox>
{
    public static readonly DependencyProperty RegularExpressionProperty =
        DependencyProperty.Register(
            "RegularException",
            typeof(string),
            typeof(TextBoxInputRegExBehavior),
            new FrameworkPropertyMetadata(".*"));

    public static readonly DependencyProperty MaxLengthProperty =
        DependencyProperty.Register(
            "MaxLength",
            typeof(int),
            typeof(TextBoxInputRegExBehavior),
            new FrameworkPropertyMetadata(int.MaxValue));

    public static readonly DependencyProperty EmptyValueProperty =
        DependencyProperty.Register(
            "EmptyValue",
            typeof(string),
            typeof(TextBoxInputRegExBehavior),
            null);

    public string RegularExpression
    {
        get => (string)GetValue(RegularExpressionProperty);
        set => SetValue(RegularExpressionProperty, value);
    }

    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    public string EmptyValue
    {
        get => (string)GetValue(EmptyValueProperty);
        set => SetValue(EmptyValueProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.PreviewTextInput += PreviewTextInputHandler;
        AssociatedObject.PreviewKeyDown += PreviewKeyDownHandler;
        DataObject.RemovePastingHandler(AssociatedObject, PassingHandler);
    }


    private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
    {
        string text;

        if (AssociatedObject.Text.Length < AssociatedObject.CaretIndex)
        {
            text = AssociatedObject.Text;
        }
        else
        {
            text = TreatSelectedText(out string remainingTextAfterRemoveSelection)
                ? remainingTextAfterRemoveSelection.Insert(AssociatedObject.SelectionStart, e.Text)
                : AssociatedObject.Text.Insert(AssociatedObject.CaretIndex, e.Text);
        }

        e.Handled = !ValidateText(text);
    }

    private void PreviewKeyDownHandler(object sender, KeyEventArgs e)
    {
        if (string.IsNullOrEmpty(EmptyValue))
        {
            return;
        }

        string? text = null;

        if (e.Key == Key.Back)
        {
            if (!TreatSelectedText(out text) &&
                AssociatedObject.SelectionStart > 0)
            {
                text = AssociatedObject.Text.Remove(AssociatedObject.SelectionStart - 1, 1);
            }
        }
        else if (e.Key == Key.Delete && !TreatSelectedText(out text) &&
                AssociatedObject.Text.Length > AssociatedObject.SelectionStart)
        {
            text = AssociatedObject.Text.Remove(AssociatedObject.SelectionStart, 1);
        }

        if (string.IsNullOrEmpty(text))
        {
            AssociatedObject.Text = EmptyValue;

            if (e.Key == Key.Back)
            {
                AssociatedObject.SelectionStart++;
            }
            e.Handled = true;
        }
    }

    private void PassingHandler(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(DataFormats.Text))
        {
            var text = e.DataObject.GetData(DataFormats.Text).ToString();

            if (!ValidateText(text))
            {
                e.CancelCommand();
            }
        }
        else
        {
            e.CancelCommand();
        }
    }

    private bool TreatSelectedText(out string text)
    {
        text = null;

        if (AssociatedObject.SelectionLength <= 0)
        {
            return false;
        }

        var length = AssociatedObject.Text.Length;
        if (AssociatedObject.SelectionStart >= length)
        {
            return true;
        }

        if (AssociatedObject.SelectionStart + AssociatedObject.SelectionLength >= length)
        {
            AssociatedObject.SelectionLength = length - AssociatedObject.SelectionStart;
        }

        text = AssociatedObject.Text.Remove(AssociatedObject.SelectionStart, AssociatedObject.SelectionLength);
        return true;
    }

    private bool ValidateText(string? text)
    {
        return (new Regex(RegularExpression, RegexOptions.IgnoreCase)).IsMatch(text)
            && (MaxLength == int.MinValue || text.Length <= MaxLength);
    }
}
