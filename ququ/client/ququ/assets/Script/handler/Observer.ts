export class Observer {
    
    private callback: Function = null;
    
    private context: any = null;

    constructor(callback: Function, context: any) {
        this.callback = callback;
        this.context = context;
    }

    notify(...args: any[]): void {
        this.callback.call(this.context, ...args);
    }

    compar(context: any): boolean {
        return context == this.context;
    }
}
