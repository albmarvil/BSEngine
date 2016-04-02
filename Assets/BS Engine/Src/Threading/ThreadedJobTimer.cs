///----------------------------------------------------------------------
/// @file ThreadedJobTimer.cs
///
/// This file contains the declaration of ThreadedJobTimer class.
/// 
/// A threaded job is a job that is executed in a different thread. It has all the properties of System.Thread
/// 
/// and can be used as corutine with WaitFor function.
/// 
/// This threaded job has a timer, so the task will be interrumpted after the given processing time
/// 
/// Based on the following thread from Unity Forums:
/// 
/// http://answers.unity3d.com/questions/357033/unity3d-and-c-coroutines-vs-threading.html
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 01/12/2015
///----------------------------------------------------------------------



using UnityEngine;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace BSEngine
{
    namespace Threading
    {
        public abstract class ThreadedJobTimer : ThreadedJob
        {

            #region Private params

            private float m_maxProcessingTime = 0.0f;

            private Stopwatch m_timer = null;

            #endregion

            #region Public methods

            /// <summary>
            /// Public constructor with maximum time per thread job
            /// </summary>
            /// <param name="maxProcessingTime">Maximum thread job time</param>
            public ThreadedJobTimer(float maxProcessingTime) : base()
            {
                m_maxProcessingTime = maxProcessingTime;
            }


            /// <summary>
            /// Used to start the threading job
            /// </summary>
            public override void Start()
            {
                m_timer = new Stopwatch();
                m_Thread = new Thread(Run);

                m_timer.Start();
                m_Thread.Start();
            }


            /// <summary>
            /// Used to abort the threading job
            /// </summary>
            public override void Abort()
            {
                if (m_Thread != null)
                {
                    m_Thread.Abort();
                }

                if (m_timer != null)
                {
                    m_timer.Stop();
                }
                Aborted = true;
            }

            /// <summary>
            /// Used to update the job status
            /// </summary>
            /// <returns>Ture if the job has finished, False all the oteher cases</returns>
            public override bool Update()
            {
                if ((m_timer.ElapsedMilliseconds * 0.001) >= m_maxProcessingTime)
                {
                    //UnityEngine.Debug.LogWarning("Time exceeded on Threading Job with timer, aborting task");
                    Abort();
                }

                if (IsDone)
                {
                    OnFinished();
                    return true;
                }
                else if (Aborted)
                {
                    OnAbort();
                    return true;
                }
                return false;
            }



            #endregion

            #region Private methods

            /// <summary>
            /// Main function of the threading job
            /// </summary>
            private void Run()
            {
                ThreadFunction();
                m_timer.Stop();
                IsDone = true;
            }
            #endregion

        }
    }
}
