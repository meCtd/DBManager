using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace DBManager.Application.Utils
{
    public class SharedResourceDictionary : ResourceDictionary
    {
        /// <summary>
        /// Internal cache of loaded dictionaries.
        /// </summary>
        private static readonly Dictionary<Uri, ResourceDictionary> _sharedDictionaries =
            new Dictionary<Uri, ResourceDictionary>();

        /// <summary>
        /// A value indicating whether the application is in designer mode.
        /// </summary>
        private static readonly bool _isInDesignerMode;

        /// <summary>
        /// Local member of the source uri
        /// </summary>
        private Uri _sourceUri;

        /// <summary>
        /// Initializes static members of the <see cref="SharedResourceDictionary"/> class.
        /// </summary>
        static SharedResourceDictionary()
        {
            _isInDesignerMode = (bool) DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject))
                .DefaultValue;
        }

        /// <summary>
        /// Gets the internal cache of loaded dictionaries.
        /// </summary>
        public static Dictionary<Uri, ResourceDictionary> SharedDictionaries
        {
            get { return _sharedDictionaries; }
        }

        /// <summary>
        /// Gets or sets the uniform resource identifier (URI) to load resources from.
        /// </summary>
        public new Uri Source
        {
            get { return _sourceUri; }

            set
            {
                _sourceUri = value;

                // Always load the dictionary by default in designer mode.
                if (!_sharedDictionaries.ContainsKey(value) || _isInDesignerMode)
                {
                    // If the dictionary is not yet loaded, load it by setting
                    // the source of the base class
                    base.Source = value;

                    // add it to the cache if we're not in designer mode
                    if (!_isInDesignerMode)
                    {
                        _sharedDictionaries.Add(value, this);
                    }
                }
                else
                {
                    // If the dictionary is already loaded, get it from the cache
                    MergedDictionaries.Add(_sharedDictionaries[value]);
                }
            }
        }
    }
}
