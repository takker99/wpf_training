using System;
using System.Threading.Tasks;

namespace Sample4.StartUp.Models
{
    /// <summary>
    /// ���ɏd�������� (�ڈ���50ms)��ʂ�thread�ōs�����߂�interface
    /// </summary>
    public interface IHeavyWorker
    {
        /// <summary>
        /// ���ɏd�������������s����
        /// </summary>
        /// <param name="progressInfo">�����̒ʒm��</param>
        /// <returns></returns>
        public Task HeavyWork(IProgress<ProgressInfo> progressInfo);

        /// <summary>
        /// �����𒆒f����
        /// </summary>
        public void Cancel();
    }
}
