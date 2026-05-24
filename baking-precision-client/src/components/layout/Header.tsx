import { Search, UserCircle } from "lucide-react";

export default function Header() {
  return (
    <header className="h-16 bg-white border-b border-slate-200 flex items-center justify-between px-8 shrink-0">
      <div className="flex items-center text-slate-500 bg-slate-100 px-3 py-2 rounded-md w-96 focus-within:ring-2 focus-within:ring-blue-500 focus-within:bg-white transition-all">
        <Search size={18} className="mr-2 text-slate-400" />
        <input
          type="text"
          placeholder="Search recipes, ingredients..."
          className="bg-transparent border-none outline-none w-full text-sm text-slate-900 placeholder:text-slate-500"
        />
      </div>

      <div className="flex items-center gap-4 text-slate-600">
        <button className="hover:text-blue-600 transition-colors">
          <UserCircle size={28} />
        </button>
      </div>
    </header>
  );
}
