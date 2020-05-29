using Microsoft.JScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace PRNG {
    public class NewPRNG {
        public class Masher {
            double mashN = (double)0xefc8249d;
            public double Floor(double a) {
                return Math.Floor(a);
            }

            public double Mash(string data = "") {
                if (data != null && data != "") {
                    //Console.WriteLine("Masing: '" + data + "'");
                    for (int i = 0; i < data.Length; i++) {
                        mashN += data[i];
                        double h = 0.02519603282416938 * mashN;
                        mashN = Floor(h);
                        h -= mashN;
                        h *= mashN;
                        mashN = Floor(h);
                        h -= mashN;
                        mashN += (double)(h * 0x100000000);
                    }
                    return Floor(mashN) * 2.3283064365386963e-10;
                } else {
                    mashN = (double)0xefc8249d;
                    return 0;
                }
            }
        }

        double rawprng() {
            p++;
            if (p >= o) {
                p = 0;
            }
            double t = 1768863d * s[p] + ((double)c * 2.3283064365386963e-10d);
            //Console.WriteLine("raw prng: " + t);
            c = (int)t | 0;
            s[p] = t - (double)c;
            return s[p];
        }

        public double random() {
            return random(double.MaxValue - 1) / double.MaxValue;
        }

        public double random(double range) {
            return Math.Floor(range * (rawprng() + (rawprng() * (double)0x200000) * 1.1102230246251565e-16));
        }

        void initState() {
            mash.Mash();
            for (int i = 0; i < o; i++) {
                s[i] = mash.Mash(" ");
            }
            c = 1;
            p = o;
        }

        void hashString() {
            string seed = this.seed.Trim();
            mash.Mash(seed);
            char[] arr = seed.ToCharArray();
            for (int i = 0; i < arr.Length; i++) {
                for (int j = 0; j < o; j++) {
                    s[j] -= mash.Mash(((int)arr[i]).ToString());
                    #warning sometimes if needs to be changed to a while if the output becomes non-zero!
                    if (s[j] < 0) {
                        s[j] += 1;
                    }
                }
            }
        }

        int o = 48;
        int c = 1;
        int p = 48;
        double[] s = new double[48];
        string seed;
        Masher mash;

        //public NewPRNG(dynamic seed) : this((string)seed.ToString()) { }

        public NewPRNG(string seed) {
            this.seed = seed;
            mash = new Masher();

            initState();
            hashString();
        }
    }
}
