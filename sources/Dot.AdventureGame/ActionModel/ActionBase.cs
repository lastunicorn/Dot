using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DustInTheWind.Dot.AdventureGame.ActionModel
{
    public abstract class ActionBase
    {
        public string Name => Names.Count > 0 ? Names[0] : null;

        public List<string> Names { get; }
        
        public abstract string Description { get; }
        
        public abstract List<string> Usage { get; }
        
        public abstract ActionType ActionType { get; }
        
        private List<Regex> matchers;

        private IEnumerable<Regex> Matchers
        {
            get
            {
                if (matchers == null)
                    matchers = CreateMatchers() ?? new List<Regex>();

                return matchers;
            }
        }

        protected ActionBase(params string[] names)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));
            Names = new List<string>(names);
        }

        protected abstract List<Regex> CreateMatchers();

        public ActionInfo? Parse(string command)
        {
            string trimmedCommand = command.Trim();

            Match match = Matchers
                .Select(x => x.Match(trimmedCommand))
                .FirstOrDefault(x => x.Success);

            if (match == null)
                return null;

            return new ActionInfo
            {
                Action = this,
                Parameters = ExtractParameters(match)?.Cast<object>().ToArray()
            };
        }

        protected abstract string[] ExtractParameters(Match match);

        public abstract IEnumerable Execute(params object[] parameters);
    }
}