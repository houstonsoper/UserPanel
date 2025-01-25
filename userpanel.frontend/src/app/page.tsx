import Link from "next/link";

export default function HomePage() {
    return (
        <div className="container m-auto">
            <div className="flex h-screen">
                <div className="m-auto flex flex-col w-3/4 max-w-96 gap-2">
                    <Link href="/login">
                        <button className="bg-blue-300 text-white font-semibold p-2 w-full">
                            Login
                        </button>
                    </Link>
                    <Link href="/register">
                        <button className="bg-blue-500 text-white font-semibold p-2 w-full">
                            Register
                        </button>
                    </Link>
                </div>
            </div>
        </div>
    )
}