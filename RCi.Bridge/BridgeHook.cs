﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RCi.Bridge
{
    /// <summary>
    /// Static class ensuring all available adapters are linked.
    /// </summary>
    public static class BridgeHook
    {
        /// <summary>
        /// Flag representing singleton hook.
        /// </summary>
        private static bool HookInstalled { get; set; }

        /// <summary>
        /// Ensure adapters are linked and hook to find new adapters in freshly loaded assemblies is installed.
        /// </summary>
        internal static void EnsureHook()
        {
            // check if we have global hook installed
            if (HookInstalled)
            {
                // we are good to go
                return;
            }

            // flag singleton hook immediately,
            // because loading adapters might point back to bridge
            // which will invoke this method again
            HookInstalled = true;

            // get currently loaded assemblies
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // init hook (so that all new assemblies would be linked)
            AppDomain.CurrentDomain.AssemblyLoad += (sender, args) =>
            {
                // link adapters from new assembly
                LinkAdapters(args.LoadedAssembly);
            };

            // link adapters from all loaded assemblies
            foreach (var assembly in assemblies)
            {
                LinkAdapters(assembly);
            }
        }

        /// <summary>
        /// Find all adapters in assembly and link to bridge.
        /// </summary>
        public static void LinkAdapters(System.Reflection.Assembly assembly)
        {
            var typeIBridgeAdapter = typeof(IBridgeAdapter);

            // let's query all adapter types
            IEnumerable<Type> typesFiltered;
            try
            {
                typesFiltered = assembly.GetTypes().Where(type =>
                    !type.IsInterface &&
                    !type.IsAbstract &&
                    !type.IsGenericTypeDefinition &&
                    typeIBridgeAdapter.IsAssignableFrom(type)
                );
            }
            catch (System.Reflection.ReflectionTypeLoadException e)
            {
                typesFiltered = e.Types.Where(type => type != null);
            }

            // create instance for each adapter and link to the bridge
            foreach (var type in typesFiltered)
            {
                // try to get parameterless constructor
                var constructorInfo = type.GetConstructor(Type.EmptyTypes);

                // ensure we got it
                if (constructorInfo == null)
                {
                    throw new BridgeAdapterInitializationException(string.Format(
                        "Bridge adapter of type '{0}' does not have parameterless constructor.", type));
                }

                // create adapter
                var instance = constructorInfo.Invoke(null);

                // softcast to interface, so we could link it
                var bridgeAdapter = instance as IBridgeAdapter;

                if (bridgeAdapter == null)
                {
                    // this shouldn't appear ever, unless we screwed at filtering our types
                    throw new BridgeAdapterInitializationException(string.Format(
                        "Bridge adapter of type '{0}' does not implement '{1}'.", type, typeIBridgeAdapter));
                }

                // link to the bridge
                bridgeAdapter.LinkToBridge();
            }
        }
    }
}
