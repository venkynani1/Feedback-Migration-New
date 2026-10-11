import { Navigate, Outlet, Route, Routes, useLocation } from "react-router-dom";
import { useAuth } from "./auth";
import { Layout } from "./components/Layout";
import { Loading } from "./components/ui";
import { FeedbackForm, MyFeedback, ReviewFeedback, SupervisorFeedback } from "./pages/Feedback";
import { Home } from "./pages/Home";
import { Login } from "./pages/Login";
import { CourseMaster, EmployeeSearch, MailMaster, TrainerMaster, VendorMaster } from "./pages/Masters";
import {
  CommentsReport,
  DefaultersReport,
  EffectivenessReport,
  IndividualReport,
  OverallReport,
} from "./pages/Reports";
import { NewSession, ParticipantList } from "./pages/Sessions";
import { ExcelUpload, LearningReport } from "./pages/Upload";

function SignedIn() {
  const { user, loading } = useAuth();
  const location = useLocation();
  if (loading) return <Loading />;
  if (!user) return <Navigate to="/login" replace state={{ from: location }} />;
  return <Layout />;
}

function AdminOnly() {
  const { isAdmin } = useAuth();
  return isAdmin ? <Outlet /> : <Navigate to="/feedback" replace />;
}

export function App() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route element={<SignedIn />}>
        <Route path="/feedback" element={<MyFeedback />} />
        <Route path="/feedback/record" element={<FeedbackForm />} />
        <Route path="/feedback/supervisor" element={<SupervisorFeedback />} />
        <Route path="/feedback/review" element={<ReviewFeedback />} />
        <Route element={<AdminOnly />}>
          <Route path="/" element={<Home />} />
          <Route path="/masters/mail" element={<MailMaster />} />
          <Route path="/masters/vendor" element={<VendorMaster />} />
          <Route path="/masters/trainer" element={<TrainerMaster />} />
          <Route path="/masters/course" element={<CourseMaster />} />
          <Route path="/sessions/new" element={<NewSession />} />
          <Route path="/sessions/participants" element={<ParticipantList />} />
          <Route path="/reports/comments" element={<CommentsReport />} />
          <Route path="/reports/defaulters" element={<DefaultersReport />} />
          <Route path="/reports/overall" element={<OverallReport />} />
          <Route path="/reports/individual" element={<IndividualReport />} />
          <Route path="/reports/effectiveness" element={<EffectivenessReport />} />
          <Route path="/upload" element={<ExcelUpload />} />
          <Route path="/upload/learning-report" element={<LearningReport />} />
          <Route path="/admin/employees" element={<EmployeeSearch />} />
        </Route>
        <Route path="*" element={<Navigate to="/" replace />} />
      </Route>
    </Routes>
  );
}
