var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
define(["require", "exports", "commands/commandBase", "models/database"], function(require, exports, commandBase, database) {
    var getDatabaseStatsCommand = (function (_super) {
        __extends(getDatabaseStatsCommand, _super);
        function getDatabaseStatsCommand(db) {
            _super.call(this);
            this.db = db;
        }
        getDatabaseStatsCommand.prototype.execute = function () {
            var url = "/stats";
            return this.query(url, null, this.db);
        };
        return getDatabaseStatsCommand;
    })(commandBase);

    
    return getDatabaseStatsCommand;
});
//# sourceMappingURL=getDatabaseStatsCommand.js.map