import { lazy, type ComponentType } from "react";
import { Navigate, Outlet, Route, Routes, useLocation } from "react-router-dom";
import { useAuth } from "./auth";
import { Layout } from "./components/Layout";
import { Loading } from "./components/ui";
import { Login } from "./pages/Login";

/** Load a page's code the first time it is opened, so sign-in does not wait for every screen. */
function page<M>(load: () => Promise<M>, name: keyof M) {
  return lazy(() => load().then((module) => ({ default: module[name] as ComponentType })));
}

const feedback = () => import("./pages/Feedback");
const masters = () => import("./pages/Masters");
const reports = () => import("./pages/Reports");
const sessions = () => import("./pages/Sessions");
const upload = () => import("./pages/Upload");

const Home = page(() => import("./pages/Home"), "Home");
const MyFeedback = page(feedback, "MyFeedback");
const FeedbackForm = page(feedback, "FeedbackForm");
const SupervisorFeedback = page(feedback, "SupervisorFeedback");
const ReviewFeedback = page(feedback, "ReviewFeedback");
const MailMaster = page(masters, "MailMaster");
const VendorMaster = page(masters, "VendorMaster");
const TrainerMaster = page(masters, "TrainerMaster");
const CourseMaster = page(masters, "CourseMaster");
const EmployeeSearch = page(masters, "EmployeeSearch");
const NewSession = page(sessions, "NewSession");
const ParticipantList = page(sessions, "ParticipantList");
const CommentsReport = page(reports, "CommentsReport");
const DefaultersReport = page(reports, "DefaultersReport");
const OverallReport = page(reports, "OverallReport");
const IndividualReport = page(reports, "IndividualReport");
const EffectivenessReport = page(reports, "EffectivenessReport");
const ExcelUpload = page(upload, "ExcelUpload");
const LearningReport = page(upload, "LearningReport");

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
