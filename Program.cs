using System;
using System.Drawing;
using System.Windows.Forms;

public class GuessTheNumber : Form
{
    private Label instructionLabel;
    private TextBox guessTextBox;
    private Button playAgainButton;
    private Random random = new Random();
    private int randomNumber;
    private int previousGuess;

    // Name: rami issa ID: 202411152
    public GuessTheNumber()
    {
        InitializeComponents();
        GenerateNewNumber();
    }

    private void InitializeComponents()
    {
        this.Text = "Guess the Number";
        this.Size = new Size(400, 200);

        instructionLabel = new Label
        {
            Text = "I have a number between 1 and 1000--can you guess my number? Please enter your first guess.",
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 50
        };
        this.Controls.Add(instructionLabel);

        guessTextBox = new TextBox
        {
            Dock = DockStyle.Top
        };
        guessTextBox.KeyPress += OnGuessEntered;
        this.Controls.Add(guessTextBox);

        playAgainButton = new Button
        {
            Text = "Play Again",
            Dock = DockStyle.Bottom,
            Enabled = false
        };
        playAgainButton.Click += OnPlayAgainClicked;
        this.Controls.Add(playAgainButton);
    }

    private void GenerateNewNumber()
    {
        randomNumber = random.Next(1, 1001);
        previousGuess = 0;
        this.BackColor = DefaultBackColor;
        instructionLabel.Text = "I have a number between 1 and 1000--can you guess my number? Please enter your first guess.";
        guessTextBox.Text = "";
        guessTextBox.Enabled = true;
        playAgainButton.Enabled = false;
    }

    private void OnGuessEntered(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            if (int.TryParse(guessTextBox.Text, out int userGuess))
            {
                if (userGuess == randomNumber)
                {
                    MessageBox.Show("Correct!");
                    this.BackColor = Color.Green;
                    guessTextBox.Enabled = false;
                    playAgainButton.Enabled = true;
                }
                else
                {
                    string hint = userGuess > randomNumber ? "Too High" : "Too Low";
                    instructionLabel.Text = hint;

                    if (previousGuess != 0)
                    {
                        this.BackColor = Math.Abs(userGuess - randomNumber) < Math.Abs(previousGuess - randomNumber)
                            ? Color.Red
                            : Color.Blue;
                    }

                    previousGuess = userGuess;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }

    private void OnPlayAgainClicked(object sender, EventArgs e)
    {
        GenerateNewNumber();
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new GuessTheNumber());
    }
}