// Licensed under the MIT License. See LICENSE in the project root for license information.

using NUnit.Framework;
using OpenAI.Images;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace OpenAI.Tests
{
    internal class TestFixture_04_Images
    {
        [UnityTest]
        public IEnumerator Test_1_GenerateImages()
        {
            yield return AwaitTestUtilities.Await(async () =>
            {
                var api = new OpenAIClient();
                Assert.IsNotNull(api.ImagesEndPoint);

                var results = await api.ImagesEndPoint.GenerateImageAsync("A house riding a velociraptor", 1, ImageSize.Small);

                Assert.IsNotNull(results);
                Assert.NotZero(results.Count);

                foreach (var result in results)
                {
                    Debug.Log(result.Key);
                }
            });
        }

        [UnityTest]
        public IEnumerator Test_2_GenerateImageEdits()
        {
            yield return AwaitTestUtilities.Await(async () =>
            {
                var api = new OpenAIClient();
                Assert.IsNotNull(api.ImagesEndPoint);
                var imageAssetPath = AssetDatabase.GUIDToAssetPath("230fd778637d3d84d81355c8c13b1999");
                var maskAssetPath = AssetDatabase.GUIDToAssetPath("0be6be2fad590cc47930495d2ca37dd6");
                var results = await api.ImagesEndPoint.CreateImageEditAsync(Path.GetFullPath(imageAssetPath), Path.GetFullPath(maskAssetPath), "A sunlit indoor lounge area with a pool containing a flamingo", 1, ImageSize.Small);

                Assert.IsNotNull(results);
                Assert.NotZero(results.Count);

                foreach (var result in results)
                {
                    Debug.Log(result.Key);
                }
            });
        }

        [UnityTest]
        public IEnumerator Test_3_GenerateImageVariations()
        {
            yield return AwaitTestUtilities.Await(async () =>
            {
                var api = new OpenAIClient();
                Assert.IsNotNull(api.ImagesEndPoint);
                var imageAssetPath = AssetDatabase.GUIDToAssetPath("230fd778637d3d84d81355c8c13b1999");
                var results = await api.ImagesEndPoint.CreateImageVariationAsync(Path.GetFullPath(imageAssetPath), 1, ImageSize.Small);

                Assert.IsNotNull(results);
                Assert.NotZero(results.Count);

                foreach (var result in results)
                {
                    Debug.Log(result.Key);
                }
            });
        }
    }
}
