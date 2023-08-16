using HamDevLib;

namespace AdifLib
{
    public static class AdifComparer
    {
        public static void CompareQSOLists(List<Qso> list1, List<Qso> list2)
        {
            Console.WriteLine("QSOs present in the first list but not in the second list:");

            foreach (var qso1 in list1)
            {
                if (!list2.Exists(qso2 => AreQSOSame(qso1, qso2)))
                {
                    PrintQSO(qso1);
                }
            }

            Console.WriteLine("\nQSOs present in the second list but not in the first list:");

            foreach (var qso2 in list2)
            {
                if (!list1.Exists(qso1 => AreQSOSame(qso1, qso2)))
                {
                    PrintQSO(qso2);
                }
            }
        }

        private static bool AreQSOSame(Qso qso1, Qso qso2)
        {
            if (qso1.QsoDate != qso2.QsoDate || qso1.Call != qso2.Call ||
                qso1.Name != qso2.Name || qso1.Mode != qso2.Mode)
            {
                return false;
            }

            if (qso1.QsoDetails.Count != qso2.QsoDetails.Count)
            {
                return false;
            }

            //foreach (var field1 in qso1.OtherFields)
            //{
            //    if (!qso2.OtherFields.TryGetValue(field1.Key, out string fieldValue2) || fieldValue2 != field1.Value)
            //    {
            //        return false;
            //    }
            //}

            return true;
        }

        private static void PrintQSO(Qso qso)
        {
            foreach (var field in qso.QsoDetails)
            {
                Console.WriteLine($"{field.Name}: {field.Value}");
            }

            Console.WriteLine("---");
        }
    }
}
