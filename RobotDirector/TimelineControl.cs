using Sce.Atf.Controls.Timelines;
using Sce.Atf.Controls.Timelines.Direct2D;

namespace RobotDirector
{
    public class _TimelineControl : D2dTimelineControl
    {
        //public _TimelineControl(ITimelineDocument timelineDocument) : base(timelineDocument)
        //{
        //}
        //public _TimelineControl(ITimelineDocument timelineDocument, D2dTimelineRenderer timelineRenderer) : base(timelineDocument, timelineRenderer)
        //{
        //}
        //public _TimelineControl(ITimelineDocument timelineDocument, D2dTimelineRenderer timelineRenderer, Sce.Atf.Controls.Timelines.TimelineConstraints timelineConstraints) : base(timelineDocument, timelineRenderer, timelineConstraints)
        //{
        //}
        public _TimelineControl(ITimelineDocument timelineDocument, D2dTimelineRenderer timelineRenderer, Sce.Atf.Controls.Timelines.TimelineConstraints timelineConstraints, bool createDefaultManipulators) : base(timelineDocument, timelineRenderer, timelineConstraints, createDefaultManipulators)
        {
        }
        public override float ConstrainFrameOffset(float offset)
        {
            return (float)System.Math.Round(offset,1);
        }
    }
}
