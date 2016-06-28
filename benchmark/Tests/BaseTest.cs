using System;
using System.Diagnostics;

namespace Benchmarks.Tests
{
    public abstract class BaseTest<T, TN> : BaseTestResult, ITest
    {
        private Stopwatch AutoMapperStopwatch { get; set; }
        private Stopwatch ExpressMapperStopwatch { get; set; }
        private Stopwatch NativeMapperStopwatch { get; set; }
        private Stopwatch OoMapperStopwatch { get; set; }
        private Stopwatch ValueInjectorStopwatch { get; set; }
        private Stopwatch MapsterStopwatch { get; set; }
        private Stopwatch TinyStopwatch { get; set; }

        private Stopwatch ObjectMapperwatch { get; set; }

        public void RunTest(int count)
        {
            AutoMapperStopwatch = new Stopwatch();
            ExpressMapperStopwatch = new Stopwatch();
            NativeMapperStopwatch = new Stopwatch();
            OoMapperStopwatch = new Stopwatch();
            ValueInjectorStopwatch = new Stopwatch();
            MapsterStopwatch = new Stopwatch();
            TinyStopwatch = new Stopwatch();
            ObjectMapperwatch = new Stopwatch();

            Count = count;

            InitAutoMapper();
            InitExpressMapper();
            InitOoMapper();
            InitValueInjectorMapper();
            InitMapsterMapper();
            InitTinyMapper();
            InitObjectMapper();
            InitNativeMapper();
            Console.WriteLine("Mapping initialization finished");

            var src = GetData();

            ExpressMapperMap(src);
            ExpressMapperStopwatch = Stopwatch.StartNew();
            ExpressMapperMap(src);
            ExpressMapperStopwatch.Stop();
            Console.WriteLine("Expressmapper mapping has been finished");
            GC.Collect(2);

            try
            {
                OoMapperStopwatch = Stopwatch.StartNew();
                OoMapperMap(src);
                OoMapperStopwatch.Stop();
                Console.WriteLine("OoMapper mapping has been finished");
            }
            catch (Exception ex)
            {
                OoMapperStopwatch.Stop();
                OoMapperStopwatch.Reset();
                Console.WriteLine("OoMapper has thrown expception!");
            }
            GC.Collect(2);

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
                ObjectMapperwatch = Stopwatch.StartNew();
                ObjectMapperMap(src);
                ObjectMapperwatch.Stop();
                Console.WriteLine("ObjectMapper mapping has been finished");
            }
            catch (Exception ex)
            {
                ObjectMapperwatch.Stop();
                ObjectMapperwatch.Reset();
                Console.WriteLine("ObjectMapper has thrown expception!");
            }
            GC.Collect(2);

            NativeMapperStopwatch = Stopwatch.StartNew();
            NativeMapperMap(src);
            NativeMapperStopwatch.Stop();
            Console.WriteLine("Native mapping has been finished");
            GC.Collect(2);
        }

        protected abstract T GetData();
        protected abstract void InitAutoMapper();
        protected abstract void InitExpressMapper();
        protected abstract void InitOoMapper();
        protected abstract void InitValueInjectorMapper();
        protected abstract void InitMapsterMapper();
        protected abstract void InitTinyMapper();
        protected abstract void InitNativeMapper();

        protected abstract void InitObjectMapper();

        protected abstract TN AutoMapperMap(T src);
        protected abstract TN ExpressMapperMap(T src);
        protected abstract TN OoMapperMap(T src);
        protected abstract TN ValueInjectorMap(T src);
        protected abstract TN MapsterMap(T src);
        protected abstract TN TinyMapperMap(T src);
        protected abstract TN NativeMapperMap(T src);

        protected abstract TN ObjectMapperMap(T src);
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
            Console.WriteLine("Expressmapper took {0} ms.", ExpressMapperStopwatch.ElapsedMilliseconds);
            AddResults("express", Count, (int)ExpressMapperStopwatch.ElapsedMilliseconds);
            if (OoMapperStopwatch.ElapsedMilliseconds == 0)
            {
                Console.WriteLine("Oomapper - not supported mapping");
                AddResults("oomapper", Count, -1);
            }
            else
            {
                Console.WriteLine("Oomapper took {0} ms.", OoMapperStopwatch.ElapsedMilliseconds);
                AddResults("oomapper", Count, (int)OoMapperStopwatch.ElapsedMilliseconds);
            }

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

            if (ObjectMapperwatch.ElapsedMilliseconds == 0)
            {
                Console.WriteLine("ObjectMapper - not supported mapping");
                AddResults("ObjectMapper", Count, -1);
            }
            else
            {
                Console.WriteLine("ObjectMapper took {0} ms.", ObjectMapperwatch.ElapsedMilliseconds);
                AddResults("ObjectMapper", Count, (int)ObjectMapperwatch.ElapsedMilliseconds);
            }

            Console.WriteLine("Native code mapping took {0} ms.", NativeMapperStopwatch.ElapsedMilliseconds);
            AddResults("native", Count, (int)NativeMapperStopwatch.ElapsedMilliseconds);
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
