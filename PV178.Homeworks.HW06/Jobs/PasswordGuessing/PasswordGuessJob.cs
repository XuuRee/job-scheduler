using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PV178.Homeworks.HW06.Jobs.PasswordGuessing
{
    internal class PasswordGuessJob : BaseJob
    {
        private const int DefaultMaxPasswordLength = 5;

        private int maxPasswordLength = DefaultMaxPasswordLength;

        private string secretPassword;

        private static readonly char[] LowerCaseLetters =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
        };

        private static readonly char[] UpperCaseLetters =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private static readonly char[] Numbers =
        {
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };

        public PasswordGuessJob(long id) : base(id)
        {
        }

        #region PasswordGeneration

        /// <summary>
        /// Generates random secret password with given maximum length
        /// </summary>
        private void GenerateSecretPassword()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var passwordLength = random.Next(3, maxPasswordLength + 1);
            var password = new char[passwordLength];

            var charset = ChooseCharset(random);
            for (var i = 0; i < passwordLength; i++)
            {
                password[i] = charset[random.Next(0, charset.Length)];
            }
            secretPassword = new string(password);
        }

        /// <summary>
        /// Randomly picks charset for generated password
        /// </summary>
        /// <param name="random">Random instance</param>
        /// <returns>Chosen charset</returns>
        private static char[] ChooseCharset(Random random)
        {
            var randomNumber = random.Next(1, 11);
            char[] charset;
            if (randomNumber <= 4)
            {
                charset = LowerCaseLetters;
            }
            else
            {
                if (randomNumber <= 8)
                {
                    charset = Numbers;
                }
                else
                {
                    charset = randomNumber == 9
                        ? LowerCaseLetters.Union(Numbers).ToArray()
                        : UpperCaseLetters.Union(Numbers).ToArray();
                }
            }
            return charset;
        }

        #endregion

        public override void InitJobArguments(string input)
        {
            if (int.TryParse(input, out int maxLength))
            {
                maxPasswordLength = maxLength;
            }
            GenerateSecretPassword();
        }


        protected override void DoWork(IProgress<string> progress, CancellationToken cancellationToken)
        {
            // TODO...
            if (cancellationToken.CanBeCanceled)
            {
                
            }
            throw new NotImplementedException();
        }

        private Task TryNumbers(CancellationToken cancellationToken)
        {
            return Task.Run(() => { Dive("", 0); });
        }

        private void Dive(string prefix, int level)
        {
            level += 1;
            foreach (char c in Numbers)
            {
                //Console.WriteLine(prefix + c);
                if ((prefix + c) == secretPassword)
                {
                    Console.WriteLine($"GOT IT: {prefix + c}");
                    Console.ReadLine();
                }
                if (level < secretPassword.Length)
                {
                    Dive(prefix + c, level);
                }
            }
        }
    }
}
