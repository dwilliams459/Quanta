using Newtonsoft.Json;
using Quanta.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace Quanta.Core.Service
{
    public class UserStoryService : BaseService
    {
        public List<UserStory> GetUserStories(string filePath)
        {
            try
            {
                if (CreateIfDoesNotExist(filePath))
                {
                    var defaults = new List<UserStory>
                    {
                        new UserStory { Id = 1, Name = "Sample User Story", SprintId = null }
                    };
                    File.WriteAllText(filePath, JsonConvert.SerializeObject(defaults, Formatting.Indented));
                }

                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<UserStory>>(json) ?? new List<UserStory>();
            }
            catch (Exception)
            {
                return new List<UserStory>();
            }
        }
    }
}
