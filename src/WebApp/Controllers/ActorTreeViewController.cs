using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorTreeViewController
    {
        public ActorTreeViewController(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        public ActorSystem ActorSystem { get; }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            var root = await ActorSystem.ActorSelection("/").ResolveOne(TimeSpan.FromSeconds(1));

            return PrintChildrenPath(root as ActorRefWithCell);
        }

        private static IEnumerable<string> PrintChildrenPath(ActorRefWithCell actor)
        {
            foreach (var item in actor.Children)
            {
                yield return item.Path.ToString();

                foreach (var s in PrintChildrenPath(item as ActorRefWithCell))
                {
                    yield return s;
                };
            }
        }

    }
}
