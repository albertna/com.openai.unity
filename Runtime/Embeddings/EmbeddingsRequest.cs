// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using OpenAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenAI.Embeddings
{
    public sealed class EmbeddingsRequest
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">
        /// Input text to get embeddings for, encoded as a string or array of tokens.
        /// To get embeddings for multiple inputs in a single request, pass an array of strings or array of token arrays.
        /// Each input must not exceed 8192 tokens in length.
        /// </param>
        /// <param name="model">
        /// The <see cref="OpenAI.Models.Model"/> to use.
        /// Defaults to: text-embedding-ada-002
        /// </param>
        /// <param name="user">
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </param>
        /// <exception cref="ArgumentNullException">A valid <see cref="input"/> string is a Required parameter.</exception>
        public EmbeddingsRequest(string input, Model model = null, string user = null)
            : this(new List<string> { input }, model, user)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">
        /// Input text to get embeddings for, encoded as a string or array of tokens.
        /// To get embeddings for multiple inputs in a single request, pass an array of strings or array of token arrays.
        /// Each input must not exceed 8192 tokens in length.
        /// </param>
        /// <param name="model">
        /// The <see cref="OpenAI.Models.Model"/> to use.
        /// Defaults to: text-embedding-ada-002
        /// </param>
        /// <param name="user">
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </param>
        /// <exception cref="ArgumentNullException">A valid <see cref="input"/> string is a Required parameter.</exception>
        public EmbeddingsRequest(IEnumerable<string> input, Model model = null, string user = null)
        {
            Input = input?.ToList();

            if (Input?.Count == 0)
            {
                throw new ArgumentNullException(nameof(input), $"Missing required {nameof(input)} parameter");
            }

            Model = model ?? new Model("text-embedding-ada-002");

            if (!Model.Contains("text-embedding"))
            {
                throw new ArgumentException(nameof(model), $"{Model} is not supported for embedding.");
            }

            User = user;
        }

        [JsonProperty("input")]
        public IReadOnlyList<string> Input { get; }

        [JsonProperty("model")]
        public string Model { get; }

        [JsonProperty("user")]
        public string User { get; }
    }
}