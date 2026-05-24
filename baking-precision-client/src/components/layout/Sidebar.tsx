import Link from "next/link";
import { BookOpen, PlusCircle, Settings, ChefHat } from "lucide-react";

export default function Sidebar() {
  return (
    <aside className="w-64 bg-slate-900 text-slate-400 flex flex-col h-full border-r border-slate-800 shrink-0">
      <div className="h-16 flex items-center px-6 border-b border-slate-800 text-white font-bold text-xl gap-2">
        <ChefHat className="text-blue-500" />
        <span>Baking Precision</span>
      </div>

      <nav className="flex-1 px-4 py-6 space-y-1">
        <Link
          href="/"
          className="flex items-center gap-3 px-3 py-2.5 rounded-md hover:bg-slate-800 hover:text-white transition-colors"
        >
          <BookOpen size={20} />
          My Recipes
        </Link>
        <Link
          href="/recipes/new"
          className="flex items-center gap-3 px-3 py-2.5 rounded-md hover:bg-slate-800 hover:text-white transition-colors"
        >
          <PlusCircle size={20} />
          Add Recipe
        </Link>
      </nav>

      <div className="p-4 border-t border-slate-800">
        <Link
          href="/settings"
          className="flex items-center gap-3 px-3 py-2.5 rounded-md hover:bg-slate-800 hover:text-white transition-colors"
        >
          <Settings size={20} />
          Settings
        </Link>
      </div>
    </aside>
  );
}
