using System;
using System.Diagnostics;

namespace Benchmarks.Tests
{
    public abstract class BaseTest<T, TN> : BaseTestResult, ITest
    {
        private Stopwatch AutoMapperStopwatch { get; set; }
        private Stopwatch NativeMapperStopwatch { get; set; }
        private Stopwatch ValueInjectorStopwatch { get; set; }
        private Stopwatch MapsterStopwatch { get; set; }

        private Stopwatch PowerMapperwatch { get; set; }
        private Stopwatch TinyStopwatch { get; set; }

        public void RunTest(int count)
        {
#if !NETCOREAPP
            ExpressMapperStopwatch = new Stopwatch();
#endif
            AutoMapperStopwatch = new Stopwatch();
            NativeMapperStopwatch = new Stopwatch();
            ValueInjectorStopwatch = new Stopwatch();
            MapsterStopwatch = new Stopwatch();
            PowerMapperwatch = new Stopwatch();
            TinyStopwatch = new Stopwatch();

            Count = count;

            InitAutoMapper();
#if !NETCOREAPP
            InitExpressMapper();
#endif
            InitValueInjectorMapper();
            InitMapsterMapper();
            InitPowerMapper();
            InitNativeMapper();
            InitTinyMapper();
            Console.WriteLine("Mapping initialization finished");

            var src = GetData();

#if !NETCOREAPP

            ExpressMapperMap(src);
            ExpressMapperStopwatch = Stopwatch.StartNew();
            ExpressMapperMap(src);
            ExpressMapperStopwatch.Stop();
            Console.WriteLine("Expressmapper mapping has been finished");
            GC.Collect(2);
#endif

            try
            {
                ValueInjectorStopwatch = Stopwatch.StartNew();
                ValueInjectorMap(src);
                ValueInjectorStopwatch.Stop();
            }
            catch (Exception ex)
            {
                ValueInjectorStopwatch.Stop();
                ValueInjectorStopwatch.Reset();
                Console.WriteLine("ValueInjector has thrown expception!");
            }
            GC.Collect(2);

            try
            {
                MapsterStopwatch = Stopwatch.StartNew();
                MapsterMap(src);
                MapsterStopwatch.Stop();
                Console.WriteLine("Mapster mapping has been finished");
            }
            catch (Exception ex)
            {
                MapsterStopwatch.Stop();
                MapsterStopwatch.Reset();
                Console.WriteLine("Mapster has thrown expception!");
            }
            GC.Collect(2);

            try
            {
                AutoMapperStopwatch = Stopwatch.StartNew();
                AutoMapperMap(src);
                AutoMapperStopwatch.Stop();
                Console.WriteLine("Automapper mapping has been finished");
            }
            catch (Exception ex)
            {
                AutoMapperStopwatch.Stop();
                AutoMapperStopwatch.Reset();
                Console.WriteLine("Automapper has thrown expception!");
            }
            GC.Collect(2);

            try
            {
                PowerMapperwatch = Stopwatch.StartNew();
                PowerMapperMap(src);
                PowerMapperwatch.Stop();
                Console.WriteLine("PowerMapper mapping has been finished");
            }
            catch (Exception ex)
            {
                PowerMapperwatch.Stop();
                PowerMapperwatch.Reset();
                Console.WriteLine("PowerMapper has thrown expception!");
            }
            GC.Collect(2);
            try
            {
                TinyStopwatch = Stopwatch.StartNew();
                TinyMapperMap(src);
                TinyStopwatch.Stop();
                Console.WriteLine("Tinymapper mapping has been finished");
            }
            catch (Exception ex)
            {
                TinyStopwatch.Stop();
                TinyStopwatch.Reset();
                Console.WriteLine("Tinymapper has thrown expception!");
            }
            GC.Collect(2);

            NativeMapperStopwatch = Stopwatch.StartNew();
            NativeMapperMap(src);
            NativeMapperStopwatch.Stop();
            Console.WriteLine("Native mapping has been finished");
            GC.Collect(2);
        }
#if !NETCOREAPP
        private Stopwatch ExpressMapperStopwatch { get; set; }
        protected abstract void InitExpressMapper();
        protected abstract TN ExpressMapperMap(T src);
#endif
        protected abstract T GetData();
        protected abstract void InitAutoMapper();
        protected abstract void InitValueInjectorMapper();
        protected abstract void InitMapsterMapper();
        protected abstract void InitNativeMapper();

        protected abstract void InitPowerMapper();
        protected abstract TN AutoMapperMap(T src);
        protected abstract TN ValueInjectorMap(T src);
        protected abstract TN MapsterMap(T src);
        protected abstract TN NativeMapperMap(T src);
        protected abstract void InitTinyMapper();
        protected abstract TN TinyMapperMap(T src);

        protected abstract TN PowerMapperMap(T src);
        //protected abstract string TestName { get; }
        //protected abstract string Size { get; }

        public void RunTestManualForEach(int count)
        {
            throw new NotImplementedException();
        }

        public void PrintResults()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Test results for {0} for collection size of {1}", TestName, Count);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Automapper took {0} ms.", AutoMapperStopwatch.ElapsedMilliseconds);
            AddResults("auto", Count, (int)AutoMapperStopwatch.ElapsedMilliseconds);

            if (ValueInjectorStopwatch.ElapsedMilliseconds == 0)
            {
                Console.WriteLine("ValueInjector - not supported mapping");
                AddResults("valueinjecter", Count, -1);
            }
            else
            {
                Console.WriteLine("ValueInjector took {0} ms.", ValueInjectorStopwatch.ElapsedMilliseconds);
                AddResults("valueinjecter", Count, (int)ValueInjectorStopwatch.ElapsedMilliseconds);
            }


            if (MapsterStopwatch.ElapsedMilliseconds == 0)
            {
                Console.WriteLine("Mapster - not supported mapping");
                AddResults("mapster", Count, -1);
            }
            else
            {
                Console.WriteLine("Mapster took {0} ms.", MapsterStopwatch.ElapsedMilliseconds);
                AddResults("mapster", Count, (int)MapsterStopwatch.ElapsedMilliseconds);
            }
            if (PowerMapperwatch.ElapsedMilliseconds == 0)
            {
                Console.WriteLine("PowerMapper - not supported mapping");
                AddResults("PowerMapper", Count, -1);
            }
            else
            {
                Console.WriteLine("PowerMapper took {0} ms.", PowerMapperwatch.ElapsedMilliseconds);
                AddResults("PowerMapper", Count, (int)PowerMapperwatch.ElapsedMilliseconds);
            }
            if (TinyStopwatch.ElapsedMilliseconds == 0)
            {
                Console.WriteLine("Tinymapper - not supported mapping");
                AddResults("tiny", Count, -1);
            }
            else
            {
                Console.WriteLine("Tinymapper took {0} ms.", TinyStopwatch.ElapsedMilliseconds);
                AddResults("tiny", Count, (int)TinyStopwatch.ElapsedMilliseconds);
            }
#if !NETCOREAPP
            
            if (ExpressMapperStopwatch.ElapsedMilliseconds == 0)
            {
                Console.WriteLine("ExpressMapper - not supported mapping");
                AddResults("ExpressMapper", Count, -1);
            }
            else
            {
                Console.WriteLine("ExpressMapper took {0} ms.", ExpressMapperStopwatch.ElapsedMilliseconds);
                AddResults("ExpressMapper", Count, (int)ExpressMapperStopwatch.ElapsedMilliseconds);
            }

#endif

            Console.WriteLine("Native code mapping took {0} ms.", NativeMapperStopwatch.ElapsedMilliseconds);
            AddResults("native", Count, (int)NativeMapperStopwatch.ElapsedMilliseconds);
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
