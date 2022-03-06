using jsiSIE;

namespace sie2sru.core
{
    public class Converter
    {
        private Dictionary<string,string> _mappings = new Dictionary<string,string>();
        private Dictionary<string, decimal> _sums = new Dictionary<string, decimal>();
        private string _observeSru = "7251";

        public Converter()
        {
            initBasToSruMappingsInk2();
            foreach(var sru in _mappings.Values)
            {
                if (!_sums.ContainsKey(sru))
                {
                    _sums.Add(sru, 0);
                }
            }
        }
        public void Convert(string fileName)
        {
            var sie = new SieDocument();
            sie.Callbacks.VER = handleVoucher;
            sie.Callbacks.IB = handleIB;

            sie.ReadDocument(fileName);

            foreach(var k in _sums.Keys)
            {
                if (_sums[k] != 0)
                {
                    Console.WriteLine(k + " " + Decimal.Round(Math.Abs(_sums[k])));
                }
            }
        }

        private void handleVoucher(SieVoucher v)
        {
            
            foreach(var r in v.Rows)
            {
                var sru = "";
                if (r.Account.SRU.Count == 0)
                {
                    sru = BasToSru(r.Account.Number, r.Amount);
                }
                else
                {
                    sru = r.Account.SRU.First();
                }
                if(sru == _observeSru)
                {
                    Console.WriteLine($"Voucher {v.Number} {r.Account.Number} {r.Amount}");
                }
                var s = _sums[sru] + r.Amount;
                _sums[sru] = s;
            }
        }
        private void handleIB(SiePeriodValue v)
        {
            if (v.YearNr != 0) return;

            var sru = "";
            if (v.Account.SRU.Count == 0)
            {
                sru = BasToSru(v.Account.Number, v.Amount);
            }
            else
            {
                sru = v.Account.SRU.First();
            }

            if (sru == _observeSru)
            {
                Console.WriteLine($"IB {v.Account.Number} {v.Amount}");
            }


            var s = _sums[sru] + Decimal.ToInt32(v.Amount);
            _sums[sru] = s;
        }
        private string BasToSru(string bas, decimal amount)
        {
            if (_mappings.ContainsKey(bas))
            {
                return _mappings[bas];
            }

            if (amount >= 0 && _mappings.ContainsKey("+" + bas))
            {
                return _mappings["+" + bas];
            }

            if (amount < 0 && _mappings.ContainsKey("-" + bas))
            {
                return _mappings["-" + bas];
            }

            throw new NotImplementedException("Can't find BAS/SRU mapping for account " + bas + " " + amount.ToString());


        }
        private void initBasToSruMappingsInk2()
        {
            _mappings = new Dictionary<string, string>();

            for (int i = 1000; i <= 1087; i++) { _mappings[i.ToString()] = "7201"; }
            for (int i = 1089; i <= 1099; i++) { _mappings[i.ToString()] = "7201"; }

            for (int i = 1088; i <= 1088; i++) { _mappings[i.ToString()] = "7202"; }

            for (int i = 1100; i <= 1119; i++) { _mappings[i.ToString()] = "7214"; }
            for (int i = 1130; i <= 1179; i++) { _mappings[i.ToString()] = "7214"; }
            for (int i = 1190; i <= 1199; i++) { _mappings[i.ToString()] = "7214"; }

            for (int i = 1200; i <= 1279; i++) { _mappings[i.ToString()] = "7215"; }
            for (int i = 1290; i <= 1299; i++) { _mappings[i.ToString()] = "7215"; }

            for (int i = 1120; i <= 1129; i++) { _mappings[i.ToString()] = "7216"; }

            for (int i = 1180; i <= 1189; i++) { _mappings[i.ToString()] = "7217"; }
            for (int i = 1280; i <= 1289; i++) { _mappings[i.ToString()] = "7217"; }

            for (int i = 1310; i <= 1319; i++) { _mappings[i.ToString()] = "7230"; }

            for (int i = 1330; i <= 1335; i++) { _mappings[i.ToString()] = "7231"; }
            for (int i = 1338; i <= 1339; i++) { _mappings[i.ToString()] = "7231"; }

            for (int i = 1350; i <= 1359; i++) { _mappings[i.ToString()] = "7233"; }
            for (int i = 1336; i <= 1337; i++) { _mappings[i.ToString()] = "7233"; }

            for (int i = 1320; i <= 1329; i++) { _mappings[i.ToString()] = "7232"; }
            for (int i = 1340; i <= 1345; i++) { _mappings[i.ToString()] = "7232"; }
            for (int i = 1348; i <= 1349; i++) { _mappings[i.ToString()] = "7232"; }

            for (int i = 1360; i <= 1369; i++) { _mappings[i.ToString()] = "7234"; }

            for (int i = 1370; i <= 1389; i++) { _mappings[i.ToString()] = "7235"; }
            for (int i = 1346; i <= 1347; i++) { _mappings[i.ToString()] = "7235"; }

            for (int i = 1410; i <= 1429; i++) { _mappings[i.ToString()] = "7241"; }

            for (int i = 1440; i <= 1449; i++) { _mappings[i.ToString()] = "7242"; }

            for (int i = 1450; i <= 1469; i++) { _mappings[i.ToString()] = "7243"; }

            for (int i = 1490; i <= 1499; i++) { _mappings[i.ToString()] = "7244"; }

            for (int i = 1470; i <= 1479; i++) { _mappings[i.ToString()] = "7245"; }

            for (int i = 1480; i <= 1489; i++) { _mappings[i.ToString()] = "7246"; }

            for (int i = 1510; i <= 1559; i++) { _mappings[i.ToString()] = "7251"; }
            for (int i = 1580; i <= 1589; i++) { _mappings[i.ToString()] = "7251"; }

            for (int i = 1560; i <= 1569; i++) { _mappings[i.ToString()] = "7252"; }
            for (int i = 1570; i <= 1572; i++) { _mappings[i.ToString()] = "7252"; }
            for (int i = 1574; i <= 1579; i++) { _mappings[i.ToString()] = "7252"; }
            for (int i = 1660; i <= 1669; i++) { _mappings[i.ToString()] = "7252"; }
            for (int i = 1671; i <= 1672; i++) { _mappings[i.ToString()] = "7252"; }
            for (int i = 1674; i <= 1679; i++) { _mappings[i.ToString()] = "7252"; }

            for (int i = 1610; i <= 1619; i++) { _mappings[i.ToString()] = "7261"; }
            for (int i = 1630; i <= 1659; i++) { _mappings[i.ToString()] = "7261"; }
            for (int i = 1680; i <= 1699; i++) { _mappings[i.ToString()] = "7261"; }
            for (int i = 1573; i <= 1573; i++) { _mappings[i.ToString()] = "7261"; }
            for (int i = 1673; i <= 1673; i++) { _mappings[i.ToString()] = "7261"; }

            for (int i = 1620; i <= 1629; i++) { _mappings[i.ToString()] = "7262"; }

            for (int i = 1700; i <= 1799; i++) { _mappings[i.ToString()] = "7263"; }

            for (int i = 1860; i <= 1869; i++) { _mappings[i.ToString()] = "7270"; }

            for (int i = 1800; i <= 1859; i++) { _mappings[i.ToString()] = "7271"; }
            for (int i = 1870; i <= 1899; i++) { _mappings[i.ToString()] = "7271"; }

            for (int i = 1900; i <= 1999; i++) { _mappings[i.ToString()] = "7281"; }

            for (int i = 2080; i <= 2089; i++) { _mappings[i.ToString()] = "7301"; }

            for (int i = 2090; i <= 2099; i++) { _mappings[i.ToString()] = "7302"; }

            for (int i = 2110; i <= 2139; i++) { _mappings[i.ToString()] = "7321"; }

            for (int i = 2150; i <= 2159; i++) { _mappings[i.ToString()] = "7322"; }

            for (int i = 2160; i <= 2199; i++) { _mappings[i.ToString()] = "7323"; }

            for (int i = 2210; i <= 2219; i++) { _mappings[i.ToString()] = "7331"; }

            for (int i = 2230; i <= 2239; i++) { _mappings[i.ToString()] = "7332"; }

            for (int i = 2220; i <= 2229; i++) { _mappings[i.ToString()] = "7333"; }
            for (int i = 2240; i <= 2299; i++) { _mappings[i.ToString()] = "7333"; }

            for (int i = 2310; i <= 2329; i++) { _mappings[i.ToString()] = "7350"; }

            for (int i = 2330; i <= 2339; i++) { _mappings[i.ToString()] = "7351"; }

            for (int i = 2340; i <= 2359; i++) { _mappings[i.ToString()] = "7352"; }

            for (int i = 2360; i <= 2372; i++) { _mappings[i.ToString()] = "7353"; }
            for (int i = 2374; i <= 2379; i++) { _mappings[i.ToString()] = "7353"; }

            for (int i = 2380; i <= 2399; i++) { _mappings[i.ToString()] = "7354"; }
            for (int i = 2373; i <= 2373; i++) { _mappings[i.ToString()] = "7354"; }

            for (int i = 2480; i <= 2489; i++) { _mappings[i.ToString()] = "7360"; }

            for (int i = 2410; i <= 2419; i++) { _mappings[i.ToString()] = "7361"; }

            for (int i = 2420; i <= 2429; i++) { _mappings[i.ToString()] = "7362"; }

            for (int i = 2430; i <= 2439; i++) { _mappings[i.ToString()] = "7363"; }

            for (int i = 2450; i <= 2459; i++) { _mappings[i.ToString()] = "7364"; }

            for (int i = 2440; i <= 2449; i++) { _mappings[i.ToString()] = "7365"; }

            for (int i = 2492; i <= 2492; i++) { _mappings[i.ToString()] = "7366"; }

            for (int i = 2460; i <= 2472; i++) { _mappings[i.ToString()] = "7367"; }
            for (int i = 2474; i <= 2872; i++) { _mappings[i.ToString()] = "7367"; }
            for (int i = 2874; i <= 2879; i++) { _mappings[i.ToString()] = "7367"; }

            for (int i = 2490; i <= 2491; i++) { _mappings[i.ToString()] = "7369"; }
            for (int i = 2493; i <= 2499; i++) { _mappings[i.ToString()] = "7369"; }
            for (int i = 2600; i <= 2859; i++) { _mappings[i.ToString()] = "7369"; }
            for (int i = 2880; i <= 2899; i++) { _mappings[i.ToString()] = "7369"; }

            for (int i = 2500; i <= 2599; i++) { _mappings[i.ToString()] = "7368"; }

            for (int i = 2900; i <= 2999; i++) { _mappings[i.ToString()] = "7370"; }

            for (int i = 2900; i <= 2999; i++) { _mappings[i.ToString()] = "7370"; }

            for (int i = 3000; i <= 3799; i++) { _mappings[i.ToString()] = "7410"; }

            for (int i = 4900; i <= 4909; i++) { _mappings["+" + i.ToString()] = "7411"; }
            for (int i = 4930; i <= 4959; i++) { _mappings["+" + i.ToString()] = "7411"; }
            for (int i = 4970; i <= 4979; i++) { _mappings["+" + i.ToString()] = "7411"; }
            for (int i = 4990; i <= 4999; i++) { _mappings["+" + i.ToString()] = "7411"; }

            for (int i = 4900; i <= 4909; i++) { _mappings["-" + i.ToString()] = "7510"; }
            for (int i = 4930; i <= 4959; i++) { _mappings["-" + i.ToString()] = "7510"; }
            for (int i = 4970; i <= 4979; i++) { _mappings["-" + i.ToString()] = "7510"; }
            for (int i = 4990; i <= 4999; i++) { _mappings["-" + i.ToString()] = "7510"; }

            for (int i = 3800; i <= 3899; i++) { _mappings[i.ToString()] = "7412"; }

            for (int i = 3900; i <= 3999; i++) { _mappings[i.ToString()] = "7413"; }

            for (int i = 4000; i <= 4799; i++) { _mappings[i.ToString()] = "7511"; }
            for (int i = 4910; i <= 4929; i++) { _mappings[i.ToString()] = "7511"; }

            //for (int i = 4960; i <= 4969; i++) { _mappings[i.ToString()] = "7512"; }
            _mappings["placeholder-sru-7512"] = "7512";
            //for (int i = 4980; i <= 4989; i++) { _mappings[i.ToString()] = "7512"; }

            for (int i = 5000; i <= 6999; i++) { _mappings[i.ToString()] = "7513"; }

            for (int i = 7000; i <= 7699; i++) { _mappings[i.ToString()] = "7514"; }

            for (int i = 7700; i <= 7739; i++) { _mappings[i.ToString()] = "7515"; }
            for (int i = 7750; i <= 7789; i++) { _mappings[i.ToString()] = "7515"; }
            for (int i = 7800; i <= 7889; i++) { _mappings[i.ToString()] = "7515"; }

            for (int i = 7740; i <= 7749; i++) { _mappings[i.ToString()] = "7516"; }
            for (int i = 7790; i <= 7799; i++) { _mappings[i.ToString()] = "7516"; }

            for (int i = 7900; i <= 7999; i++) { _mappings[i.ToString()] = "7517"; }

            for (int i = 8000; i <= 8069; i++) { _mappings["+" + i.ToString()] = "7414"; }
            for (int i = 8090; i <= 8099; i++) { _mappings["+" + i.ToString()] = "7414"; }

            for (int i = 8000; i <= 8069; i++) { _mappings["-" + i.ToString()] = "7518"; }
            for (int i = 8090; i <= 8099; i++) { _mappings["-" + i.ToString()] = "7518"; }

            for (int i = 8100; i <= 8112; i++) { _mappings["+" + i.ToString()] = "7415"; }
            for (int i = 8114; i <= 8117; i++) { _mappings["+" + i.ToString()] = "7415"; }
            for (int i = 8119; i <= 8122; i++) { _mappings["+" + i.ToString()] = "7415"; }
            for (int i = 8124; i <= 8132; i++) { _mappings["+" + i.ToString()] = "7415"; }
            for (int i = 8134; i <= 8169; i++) { _mappings["+" + i.ToString()] = "7415"; }
            for (int i = 8190; i <= 8199; i++) { _mappings["+" + i.ToString()] = "7415"; }

            for (int i = 8100; i <= 8112; i++) { _mappings["-" + i.ToString()] = "7519"; }
            for (int i = 8114; i <= 8117; i++) { _mappings["-" + i.ToString()] = "7519"; }
            for (int i = 8119; i <= 8122; i++) { _mappings["-" + i.ToString()] = "7519"; }
            for (int i = 8124; i <= 8132; i++) { _mappings["-" + i.ToString()] = "7519"; }
            for (int i = 8134; i <= 8169; i++) { _mappings["-" + i.ToString()] = "7519"; }
            for (int i = 8190; i <= 8199; i++) { _mappings["-" + i.ToString()] = "7519"; }

            for (int i = 8113; i <= 8113; i++) { _mappings["+" + i.ToString()] = "7423"; }
            for (int i = 8118; i <= 8118; i++) { _mappings["+" + i.ToString()] = "7423"; }
            for (int i = 8123; i <= 8123; i++) { _mappings["+" + i.ToString()] = "7423"; }
            for (int i = 8133; i <= 8133; i++) { _mappings["+" + i.ToString()] = "7423"; }

            for (int i = 8113; i <= 8113; i++) { _mappings["-" + i.ToString()] = "7530"; }
            for (int i = 8118; i <= 8118; i++) { _mappings["-" + i.ToString()] = "7530"; }
            for (int i = 8123; i <= 8123; i++) { _mappings["-" + i.ToString()] = "7530"; }
            for (int i = 8133; i <= 8133; i++) { _mappings["-" + i.ToString()] = "7530"; }

            for (int i = 8200; i <= 8269; i++) { _mappings["+" + i.ToString()] = "7416"; }
            for (int i = 8290; i <= 8299; i++) { _mappings["+" + i.ToString()] = "7416"; }

            for (int i = 8200; i <= 8269; i++) { _mappings["-" + i.ToString()] = "7520"; }
            for (int i = 8290; i <= 8299; i++) { _mappings["-" + i.ToString()] = "7520"; }

            for (int i = 8300; i <= 8369; i++) { _mappings[i.ToString()] = "7417"; }
            for (int i = 8390; i <= 8399; i++) { _mappings[i.ToString()] = "7417"; }

            for (int i = 8070; i <= 8079; i++) { _mappings[i.ToString()] = "7521"; }
            for (int i = 8080; i <= 8089; i++) { _mappings[i.ToString()] = "7521"; }
            for (int i = 8170; i <= 8179; i++) { _mappings[i.ToString()] = "7521"; }
            for (int i = 8180; i <= 8189; i++) { _mappings[i.ToString()] = "7521"; }
            for (int i = 8270; i <= 8279; i++) { _mappings[i.ToString()] = "7521"; }
            for (int i = 8280; i <= 8289; i++) { _mappings[i.ToString()] = "7521"; }
            for (int i = 8370; i <= 8379; i++) { _mappings[i.ToString()] = "7521"; }
            for (int i = 8380; i <= 8389; i++) { _mappings[i.ToString()] = "7521"; }

            for (int i = 8400; i <= 8499; i++) { _mappings[i.ToString()] = "7522"; }
            
            for (int i = 8830; i <= 8839; i++) { _mappings[i.ToString()] = "7524"; }

            for (int i = 8820; i <= 8829; i++) { _mappings[i.ToString()] = "7419"; }

            for (int i = 8810; i <= 8810; i++) { _mappings["+" + i.ToString()] = "7420"; }
            for (int i = 8819; i <= 8819; i++) { _mappings[i.ToString()] = "7420"; }

            for (int i = 8810; i <= 8810; i++) { _mappings["-" + i.ToString()] = "7525"; }
            for (int i = 8811; i <= 8811; i++) { _mappings[i.ToString()] = "7525"; }

            for (int i = 8850; i <= 8859; i++) { _mappings["+" + i.ToString()] = "7421"; }
            for (int i = 8850; i <= 8859; i++) { _mappings["-" + i.ToString()] = "7526"; }

            for (int i = 8860; i <= 8899; i++) { _mappings["+" + i.ToString()] = "7422"; }
            for (int i = 8860; i <= 8899; i++) { _mappings["-" + i.ToString()] = "7527"; }
            for (int i = 8840; i <= 8849; i++) { _mappings["-" + i.ToString()] = "7527"; }

            for (int i = 8900; i <= 8989; i++) { _mappings[i.ToString()] = "7528"; }

            for (int i = 8990; i <= 8999; i++) { _mappings["+" + i.ToString()] = "7450"; }
            for (int i = 8990; i <= 8999; i++) { _mappings["-" + i.ToString()] = "7550"; }

        }
    }
}
