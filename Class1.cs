namespace JTypes
{
    public class ReversibleDictionary<T1, T2>
    {
        List<T1> L1 = new List<T1>();
        List<T2> L2 = new List<T2>();
        public int Count
        {
            get { return L1.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="t">if t = 0,then the T1 as key,else T2 as key.</param>
        /// <returns></returns>
        public ValueTuple<T1, T2> this[object v, int t = 0]
        {
            get
            {
                if (t == 0)
                {
                    int id = L1.IndexOf((T1)v);
                    return (L1[id], L2[id]);
                }
                else
                {
                    int id = L2.IndexOf((T2)v);
                    return (L1[id], L2[id]);
                }
            }
        }

        public ValueTuple<T1, T2> this[int index]
        {
            get
            {
                return (L1[index], L2[index]);
            }
        }

        /// <summary>
        /// Add an item, but return the older data which has errors with the setting data,the legth is always 2 or null(0), you should ensure isn't null.
        /// and always 0:T1 err,1:T2 err,0&1:both err
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public ValueTuple<T1, T2>[]? Add(T1 v1, T2 v2)
        {
            int l1id = L1.IndexOf(v1);
            int l2id = L2.IndexOf(v2);
            var tmpvts = new ValueTuple<T1, T2>[2];
            bool iserr = false;
            if (l1id != -1)
            {
                tmpvts[0] = (L1[l1id], L2[l1id]);
                iserr = true;
            }
            if (l2id != -1)
            {
                tmpvts[1] = (L1[l2id], L2[l2id]);
                iserr = true;
            }
            if (!iserr)
            {
                L1.Add(v1);
                L2.Add(v2);
                return null;
            }
            else
                return tmpvts;
        }

        /// <summary>
        /// unless you are sure about it that no data errors, to use Add(T1,T2)
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public void Add_unsafe(T1 v1, T2 v2)
        {
            L1.Add(v1); L2.Add(v2);
        }

        /// <summary>
        /// Unless you are sure about that no repeatings with T1(left value),to use Add().
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns>
        /// if T2(v2) exists, return to out the T1(v1) that paired with T2(v2).
        /// otherwise, retrun the default value.
        /// </returns>
        public T1? Add_out1(T1 v1, T2 v2)
        {
            int id = L2.IndexOf(v2);
            if (id != -1)
                return L1[id];
            Add_unsafe(v1, v2);
            return default(T1);
        }
        /// <summary>
        /// Unless you are sure about that no repeatings with T2(right value),to use Add().
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns>
        /// if T1(v1) exists, return to out the T2(v2) that paired with T1(v1).
        /// otherwise, retrun the default value.
        /// </returns>
        public T2? Add_out2(T1 v1, T2 v2)
        {
            int id = L1.IndexOf(v1);
            if (id != -1)
                return L2[id];
            Add_unsafe(v1, v2);
            return default(T2);
        }
        public ValueTuple<T1, T2>? Find1(T1 value)
        {
            int id = L1.IndexOf(value);
            if (id == -1)
                return null;
            return (L1[id], L2[id]);
        }
        public ValueTuple<T1, T2>? Find2(T2 value)
        {
            int id = L2.IndexOf(value);
            if (id == -1)
                return null;
            return (L1[id], L2[id]);
        }
    }
}