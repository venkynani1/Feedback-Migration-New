import type { IconName } from "./components/Icon";

export interface NavItem {
  to: string;
  label: string;
  icon: IconName;
  description: string;
}

export interface NavGroup {
  label: string;
  items: NavItem[];
}

/** The administrator menu, in the order of the old site's menu bar. */
export const ADMIN_NAV: NavGroup[] = [
  {
    label: "Masters",
    items: [
      { to: "/masters/mail", label: "Mail Master", icon: "mail", description: "View and update the notification mail templates." },
      { to: "/masters/vendor", label: "Vendor Master", icon: "building", description: "Register training vendors." },
      { to: "/masters/trainer", label: "Trainer Master", icon: "user", description: "Register internal and external trainers for a course." },
      { to: "/masters/course", label: "Course Master", icon: "book", description: "Add a new course." },
    ],
  },
  {
    label: "Session",
    items: [
      { to: "/sessions/new", label: "Create New Session", icon: "calendar", description: "Schedule a session and assign its trainers." },
      { to: "/sessions/participants", label: "Upload Participant List", icon: "users", description: "Nominate participants and send the feedback mail." },
    ],
  },
  {
    label: "Reports",
    items: [
      { to: "/reports/comments", label: "View Comments", icon: "comment", description: "Review comments and approve them for the analysis report." },
      { to: "/reports/defaulters", label: "Feedback Defaulters", icon: "alert", description: "Participants who still owe feedback." },
      { to: "/reports/overall", label: "Overall Reports", icon: "chart", description: "Section scores per session or trainer, with PDF export." },
      { to: "/reports/individual", label: "Individual Reports", icon: "list", description: "Answers per participant, with Excel export." },
      { to: "/reports/effectiveness", label: "Training Effectiveness Report", icon: "trend", description: "Supervisor ratings for the Fresher Training Program." },
    ],
  },
  {
    label: "Feedback",
    items: [
      { to: "/feedback", label: "Record My Feedback", icon: "edit", description: "Your pending feedback, ratings and reviews." },
    ],
  },
  {
    label: "Excel Upload",
    items: [
      { to: "/upload", label: "Excel Data Upload", icon: "upload", description: "Load, view or delete a month of learning data." },
      { to: "/upload/learning-report", label: "Learning Program Report", icon: "table", description: "Learning programme and assessment completion." },
    ],
  },
];

export const SUPER_ADMIN_NAV: NavGroup = {
  label: "Administration",
  items: [
    { to: "/admin/employees", label: "Employee Search", icon: "search", description: "Look up an employee by id or name." },
  ],
};

/** The menu entry a path belongs to; sub-pages such as /feedback/record match their parent. */
export function findNav(pathname: string): { group: NavGroup; item: NavItem } | null {
  let best: { group: NavGroup; item: NavItem } | null = null;
  for (const group of [...ADMIN_NAV, SUPER_ADMIN_NAV]) {
    for (const item of group.items) {
      const matches = pathname === item.to || pathname.startsWith(`${item.to}/`);
      if (matches && (!best || item.to.length > best.item.to.length)) best = { group, item };
    }
  }
  return best;
}
